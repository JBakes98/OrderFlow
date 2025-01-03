using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain;
using Orderflow.Domain.Commands;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Mappers;
using Orderflow.Services.AlphaVantage;
using Serilog;
using Error = Orderflow.Domain.Models.Error;

namespace Orderflow.Services;

public class OrderService : IOrderService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Order, OrderRaisedEvent> _orderToOrderRaisedEventMapper;
    private readonly IMapper<OrderUpdateCommand, OrderUpdateEvent> _orderUpdateCommandToOrderUpdateEventMapper;
    private readonly IOrderRepository _repository;
    private readonly IAmazonS3 _s3;
    private readonly IAlphaVantageService _alphaVantageService;
    private readonly IInstrumentService _instrumentService;
    private readonly IOrderBookManager _orderBookManager;

    public OrderService(
        IOrderRepository repository,
        IMapper<Order, OrderRaisedEvent> orderToOrderRaisedEventMapper,
        IDiagnosticContext diagnosticContext,
        IAmazonS3 s3,
        IMapper<OrderUpdateCommand, OrderUpdateEvent> orderUpdateCommandToOrderUpdateEventMapper,
        IAlphaVantageService alphaVantageService,
        IInstrumentService instrumentService,
        IOrderBookManager orderBookManager)
    {
        _orderBookManager = Guard.Against.Null(orderBookManager);
        _instrumentService = Guard.Against.Null(instrumentService);
        _alphaVantageService = Guard.Against.Null(alphaVantageService);
        _orderUpdateCommandToOrderUpdateEventMapper = Guard.Against.Null(orderUpdateCommandToOrderUpdateEventMapper);
        _s3 = Guard.Against.Null(s3);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _orderToOrderRaisedEventMapper = Guard.Against.Null(orderToOrderRaisedEventMapper);
        _repository = Guard.Against.Null(repository);
    }


    public async Task<OneOf<Order, Error>> RetrieveOrder(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0.ToList();
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> RetrieveInstrumentOrders(string instrumentId)
    {
        var result = await _repository.GetInstrumentOrders(instrumentId);

        if (result.IsT1)
            return result.AsT1;

        return result;
    }

    public async Task<OneOf<Order, Error>> CreateOrder(Order order)
    {
        var instrumentResult = await _instrumentService.RetrieveInstrument(order.InstrumentId);
        if (instrumentResult.TryPickT1(out var instrumentError, out var instrument))
            return instrumentError;

        var quoteResult = await _alphaVantageService.GetStockQuote(instrument.Ticker);
        if (quoteResult.TryPickT1(out var quoteError, out var quote))
            return quoteError;

        order.SetPrice(quote.Price);

        var orderEvent = _orderToOrderRaisedEventMapper.Map(order);

        var error = await _repository.InsertAsync(order, orderEvent);

        if (error != null)
            return error;

        _diagnosticContext.Set("OrderEntity", order, true);
        _diagnosticContext.Set("OrderEvent", orderEvent, true);
        _diagnosticContext.Set("OrderPlaced", true);

        var orderBook = _orderBookManager.GetOrderBook(order.InstrumentId);
        var trades = orderBook.AddOrder(order);

        _diagnosticContext.Set("Trades", trades, true);

        return order;
    }

    public async Task<OneOf<Order, Error>> UpdateOrder(OrderUpdateCommand command)
    {
        var repoResult = await _repository.GetByIdAsync(command.Id);

        if (repoResult.TryPickT1(out var error, out var order))
            return error;

        if (!order.SetStatus(command.Status))
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderCouldNotBeUpdated);

        var updateEvent = _orderUpdateCommandToOrderUpdateEventMapper.Map(command);

        var updateResult = await _repository.UpdateAsync(order, updateEvent);

        return updateResult != null ? repoResult : order;
    }

    public async Task<Error> ProcessOrderHistory(IFormFile orderFile)
    {
        var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3, "orderflow-bulk-processing-bucket");

        if (!bucketExist)
        {
            _diagnosticContext.Set("BucketExist", false);
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.SpecifiedBucketDoesNotExist);
        }

        _diagnosticContext.Set("BucketExist", true);

        await using var stream = orderFile.OpenReadStream();

        var request = new PutObjectRequest
        {
            BucketName = "orderflow-bulk-processing-bucket",
            Key = $"orderform-{DateTime.Now:O}",
            InputStream = stream,
            AutoCloseStream = true,
            ContentType = orderFile.ContentType
        };

        try
        {
            await _s3.PutObjectAsync(request);
        }
        catch (AmazonS3Exception e)
        {
            _diagnosticContext.Set("S3UploadError", e);
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderFileUploadFailed);
        }

        return null!;
    }
}