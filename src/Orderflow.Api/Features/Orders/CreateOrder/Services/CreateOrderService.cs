using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Api.Routes.Instrument.GetInstrument.Services;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Order;
using Orderflow.Mappers;
using Orderflow.Services.AlphaVantage;
using Orderflow.Services.Interfaces;
using Serilog;
using Error = Orderflow.Domain.Models.Error;

namespace Orderflow.Services;

public class CreateOrderService : ICreateOrderService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Order, OrderRaisedEvent> _orderToOrderRaisedEventMapper;
    private readonly IOrderRepository _repository;
    private readonly IAlphaVantageService _alphaVantageService;
    private readonly IGetInstrumentService _getInstrumentService;
    private readonly IOrderBookManager _orderBookManager;
    private readonly ITradeService _tradeService;

    public CreateOrderService(
        IOrderRepository repository,
        IMapper<Order, OrderRaisedEvent> orderToOrderRaisedEventMapper,
        IDiagnosticContext diagnosticContext,
        IAlphaVantageService alphaVantageService,
        IGetInstrumentService getInstrumentService,
        IOrderBookManager orderBookManager,
        ITradeService tradeService)
    {
        _tradeService = Guard.Against.Null(tradeService);
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
        await _tradeService.ProcessTrades(trades);

        return order;
    }
}