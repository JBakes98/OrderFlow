using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Domain.Models;
using Serilog;
using Error = OrderFlow.Domain.Models.Error;

namespace OrderFlow.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper<Order, OrderRaisedEvent> _orderToOrderRaisedEventMapper;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IAmazonS3 _s3;

    public OrderService(
        IOrderRepository repository,
        IMapper<Order, OrderRaisedEvent> orderToOrderRaisedEventMapper,
        IDiagnosticContext diagnosticContext,
        IAmazonS3 s3)
    {
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
        _diagnosticContext.Set($"OrderEntity", order, true);
        _diagnosticContext.Set($"OrderRaised", true);

        var orderEvent = _orderToOrderRaisedEventMapper.Map(order);

        var error = await _repository.InsertAsync(order, orderEvent);

        if (error != null)
            return error;

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