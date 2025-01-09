using Amazon.S3;
using AutoFixture;
using Moq;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Commands;
using Orderflow.Domain.Models;
using Orderflow.Events.Order;
using Orderflow.Mappers;
using Orderflow.Services;
using Orderflow.Services.AlphaVantage;
using Orderflow.Services.Interfaces;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Services;

public class OrderServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IOrderRepository> _mockRepository;
    private readonly Mock<IMapper<Order, OrderRaisedEvent>> _mockOrderEventMapper;
    private readonly Mock<IMapper<OrderUpdateCommand, OrderUpdateEvent>> _mockUpdateEventMapper;
    private readonly Mock<IDiagnosticContext> _mockDiagnosticContext;
    private readonly Mock<IAmazonS3> _mockS3;
    private readonly Mock<IAlphaVantageService> _mockAlphaVantageService;
    private readonly Mock<IInstrumentService> _mockInstrumentService;
    private readonly Mock<IOrderBookManager> _mockOrderBookManager;
    private readonly Mock<ITradeService> _mockTradeService;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<IOrderRepository>();
        _mockOrderEventMapper = new Mock<IMapper<Order, OrderRaisedEvent>>();
        _mockUpdateEventMapper = new Mock<IMapper<OrderUpdateCommand, OrderUpdateEvent>>();
        _mockDiagnosticContext = new Mock<IDiagnosticContext>();
        _mockS3 = new Mock<IAmazonS3>();
        _mockAlphaVantageService = new Mock<IAlphaVantageService>();
        _mockInstrumentService = new Mock<IInstrumentService>();
        _mockOrderBookManager = new Mock<IOrderBookManager>();
        _mockTradeService = new Mock<ITradeService>();

        _orderService = new OrderService(
            _mockRepository.Object,
            _mockOrderEventMapper.Object,
            _mockDiagnosticContext.Object,
            _mockS3.Object,
            _mockUpdateEventMapper.Object,
            _mockAlphaVantageService.Object,
            _mockInstrumentService.Object,
            _mockOrderBookManager.Object,
            _mockTradeService.Object);
    }

    [Fact]
    public async Task RetrieveOrder_ShouldReturnOrder_WhenFound()
    {
        // Arrange
        var orderId = _fixture.Create<string>();
        var order = _fixture.Create<Order>();
        _mockRepository.Setup(r => r.GetByIdAsync(orderId))
            .ReturnsAsync(OneOf<Order, Error>.FromT0(order));

        // Act
        var result = await _orderService.RetrieveOrder(orderId);

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(order, result.AsT0);
    }

    [Fact]
    public async Task RetrieveOrder_ShouldReturnError_WhenNotFound()
    {
        // Arrange
        var orderId = _fixture.Create<string>();
        var error = _fixture.Create<Error>();
        _mockRepository.Setup(r => r.GetByIdAsync(orderId))
            .ReturnsAsync(OneOf<Order, Error>.FromT1(error));

        // Act
        var result = await _orderService.RetrieveOrder(orderId);

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnOrder_WhenSuccessful()
    {
        // Arrange
        var order = _fixture.Create<Order>();
        var instrument = _fixture.Create<Instrument>();
        var quote = _fixture.Build<GlobalQuote>().With(q => q.Price, 100).Create();

        _mockInstrumentService.Setup(s => s.RetrieveInstrument(order.InstrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT0(instrument));
        _mockAlphaVantageService.Setup(s => s.GetStockQuote(instrument.Ticker))
            .ReturnsAsync(OneOf<GlobalQuote, Error>.FromT0(quote));

        _mockOrderEventMapper.Setup(m => m.Map(order))
            .Returns(_fixture.Create<OrderRaisedEvent>());
        _mockOrderBookManager.Setup(m => m.GetOrderBook(order.InstrumentId))
            .Returns(new OrderBook());

        _mockRepository.Setup(r => r.InsertAsync(It.IsAny<Order>(), It.IsAny<OrderRaisedEvent>()))
            .ReturnsAsync((Error)null);

        // Act
        var result = await _orderService.CreateOrder(order);

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(order, result.AsT0);
    }

    // [Fact]
    // public async Task ProcessOrderHistory_ShouldReturnError_WhenBucketDoesNotExist()
    // {
    //     // Arrange
    //     _mockS3.Setup(s => AmazonS3Util.DoesS3BucketExistV2Async(_mockS3.Object, "orderflow-bulk-processing-bucket"))
    //         .ReturnsAsync(false);
    //
    //     var fileMock = new Mock<IFormFile>();
    //     var fileName = "test.txt";
    //     fileMock.Setup(f => f.FileName).Returns(fileName);
    //
    //     // Act
    //     var result = await _orderService.ProcessOrderHistory(fileMock.Object);
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Contains(HttpStatusCode.InternalServerError.ToString(), result.ErrorCodes);
    //     Assert.Contains(ErrorCodes.SpecifiedBucketDoesNotExist, result.ErrorCodes);
    // }
    //
    // [Fact]
    // public async Task ProcessOrderHistory_ShouldUploadFile_WhenBucketExists()
    // {
    //     // Arrange
    //     _mockS3.Setup(s => AmazonS3Util.DoesS3BucketExistV2Async(_mockS3.Object, "orderflow-bulk-processing-bucket"))
    //         .ReturnsAsync(true);
    //
    //     var fileMock = new Mock<IFormFile>();
    //     fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());
    //     fileMock.Setup(f => f.ContentType).Returns("text/plain");
    //
    //     _mockS3.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
    //         .Returns((Task<PutObjectResponse>)Task.CompletedTask);
    //
    //     // Act
    //     var result = await _orderService.ProcessOrderHistory(fileMock.Object);
    //
    //     // Assert
    //     Assert.Null(result); // No error
    // }
}