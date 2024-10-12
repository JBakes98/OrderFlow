using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Repositories;
using Serilog;
using Error = OrderFlow.Models.Error;

namespace OrderFlow.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;
    private readonly IEnqueueService _enqueueService;
    private readonly IMapper<Order, OrderCreatedEvent> _orderToOrderCreatedEventMapper;
    private readonly IMapper<BaseOrderEvent, Event> _orderEventToEventMapper;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IAmazonS3 _s3;

    public OrderService(IRepository<Order> repository,
        IEnqueueService enqueueService,
        IMapper<Order, OrderCreatedEvent> orderToOrderCreatedEventMapper,
        IMapper<BaseOrderEvent, Event> orderEventToEventMapper,
        IDiagnosticContext diagnosticContext,
        IAmazonS3 s3)
    {
        _s3 = Guard.Against.Null(s3);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _orderEventToEventMapper = Guard.Against.Null(orderEventToEventMapper);
        _orderToOrderCreatedEventMapper = Guard.Against.Null(orderToOrderCreatedEventMapper);
        _enqueueService = Guard.Against.Null(enqueueService);
        _repository = Guard.Against.Null(repository);
    }


    public async Task<OneOf<Order, Error>> RetrieveOrder(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0.ToList();
    }

    public async Task<OneOf<Order, Error>> CreateOrder(Order order)
    {
        var saveResult = await _repository.InsertAsync(order, default);
        _diagnosticContext.Set($"Order", order, true);

        if (saveResult.IsT1)
            return saveResult.AsT1;
        _diagnosticContext.Set($"OrderRaised", true);

        var orderEvent = _orderToOrderCreatedEventMapper.Map(order);
        var @event = _orderEventToEventMapper.Map(orderEvent);

        var eventPublished = await _enqueueService.PublishEvent(@event);

        if (!eventPublished)
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.EventCouldNotBePublished);

        return order;
    }

    public async Task<Error> ProcessOrderHistory(IFormFile orderFile)
    {
        var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3, "orderflow-bucket");

        if (!bucketExist)
        {
            _diagnosticContext.Set("BucketExist", false);
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.SpecifiedBucketDoesNotExist);
        }

        _diagnosticContext.Set("BucketExist", true);

        await using var stream = orderFile.OpenReadStream();

        var request = new PutObjectRequest
        {
            BucketName = "orderflow-bucket",
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