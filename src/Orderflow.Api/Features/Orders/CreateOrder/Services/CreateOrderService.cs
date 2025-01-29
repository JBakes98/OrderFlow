using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Features.AlphaVantage.Services;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Instruments.GetInstrument.Services;
using Orderflow.Features.Orders.Common.Interfaces;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Repositories;
using Orderflow.Features.Orders.CreateOrder.Events;
using Orderflow.Features.Trades.CreateTrade.Services;
using Serilog;
using Error = Orderflow.Features.Common.Models.Error;

namespace Orderflow.Features.Orders.CreateOrder.Services;

public class CreateOrderService : ICreateOrderService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Order, OrderRaisedEvent> _orderToOrderRaisedEventMapper;
    private readonly IOrderRepository _repository;
    private readonly IAlphaVantageService _alphaVantageService;
    private readonly IGetInstrumentService _getInstrumentService;
    private readonly IOrderBookManager _orderBookManager;
    private readonly IProcessTradeService _processTradeService;

    public CreateOrderService(
        IOrderRepository repository,
        IMapper<Order, OrderRaisedEvent> orderToOrderRaisedEventMapper,
        IDiagnosticContext diagnosticContext,
        IAlphaVantageService alphaVantageService,
        IGetInstrumentService getInstrumentService,
        IOrderBookManager orderBookManager,
        IProcessTradeService processTradeService)
    {
        _processTradeService = Guard.Against.Null(processTradeService);
        _orderBookManager = Guard.Against.Null(orderBookManager);
        _getInstrumentService = Guard.Against.Null(getInstrumentService);
        _alphaVantageService = Guard.Against.Null(alphaVantageService);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _orderToOrderRaisedEventMapper = Guard.Against.Null(orderToOrderRaisedEventMapper);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Order, Error>> CreateOrder(Order order)
    {
        var instrumentResult = await _getInstrumentService.GetInstrument(order.InstrumentId);
        if (instrumentResult.TryPickT1(out var instrumentError, out var instrument))
            return instrumentError;

        if (!double.IsPositive(order.Price))
        {
            var quoteResult = await _alphaVantageService.GetStockQuote(instrument.Ticker);
            if (quoteResult.TryPickT1(out var quoteError, out var quote))
                return quoteError;

            order.SetPrice(quote.Price);
        }

        var orderEvent = _orderToOrderRaisedEventMapper.Map(order);
        var error = await _repository.InsertAsync(order, orderEvent);

        if (error != null)
            return error;

        _diagnosticContext.Set("Order", order, true);
        _diagnosticContext.Set("OrderEvent", orderEvent, true);
        _diagnosticContext.Set("OrderPlaced", true);

        var orderBook = _orderBookManager.GetOrderBook(order.InstrumentId);
        var trades = orderBook.AddOrder(order);

        _diagnosticContext.Set("Trades", trades, true);
        await _processTradeService.ProcessTrades(trades);

        return order;
    }
}