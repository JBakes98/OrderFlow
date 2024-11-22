using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Contracts.Requests;
using Orderflow.Domain.Models;
using Orderflow.Extensions;
using Orderflow.Services.AlphaVantage;
using Serilog;
using GlobalQuote = Orderflow.Contracts.Responses.AlphaVantage.GlobalQuote;

namespace Orderflow.Services.Handlers;

public class OrderCreateHandler : IHandler<CreateOrder, Order>
{
    private readonly IAlphaVantageService _alphaVantageService;
    private readonly IMapper<CreateOrder, Order> _createOrderToOrderMapper;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<GlobalQuote, Domain.Models.GlobalQuote> _globalQuoteMapper;
    private readonly IInstrumentService _instrumentService;
    private readonly IOrderService _orderService;

    public OrderCreateHandler(
        IMapper<CreateOrder, Order> createOrderToOrderMapper,
        IInstrumentService instrumentService,
        IOrderService orderService,
        IAlphaVantageService alphaVantageService,
        IDiagnosticContext diagnosticContext,
        IMapper<GlobalQuote, Domain.Models.GlobalQuote> globalQuoteMapper)
    {
        _globalQuoteMapper = Guard.Against.Null(globalQuoteMapper);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _alphaVantageService = Guard.Against.Null(alphaVantageService);
        _createOrderToOrderMapper = Guard.Against.Null(createOrderToOrderMapper);
        _instrumentService = Guard.Against.Null(instrumentService);
        _orderService = Guard.Against.Null(orderService);
    }

    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        var instrumentResult = await _instrumentService.RetrieveInstrument(request.InstrumentId.ToString());

        if (instrumentResult.IsT1)
            return instrumentResult.AsT1;

        var instrument = instrumentResult.AsT0;
        _diagnosticContext.Set("Instrument", instrument.Ticker);

        var order = _createOrderToOrderMapper.Map(request);

        var quoteResult = await _alphaVantageService.GetStockQuote(instrument.Ticker);

        if (quoteResult.IsT1)
            return quoteResult.AsT1;

        var quote = _globalQuoteMapper.Map(quoteResult.AsT0);
        _diagnosticContext.Set("AlphaVantage:RetrievedQuote", quote, true);

        order.SetPrice(quote.Price);

        var result = await _orderService.CreateOrder(order);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }
}