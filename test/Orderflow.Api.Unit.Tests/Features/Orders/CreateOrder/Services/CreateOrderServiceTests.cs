using System.Net;
using Moq;
using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Domain.Models.Enums;
using Orderflow.Features.AlphaVantage.Services;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.GetInstrument.Services;
using Orderflow.Features.Orders.Common;
using Orderflow.Features.Orders.Common.Repositories;
using Orderflow.Features.Orders.CreateOrder.Events;
using Orderflow.Features.Orders.CreateOrder.Services;
using Orderflow.Features.Trades.Common;
using Orderflow.Features.Trades.CreateTrade.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Orders.CreateOrder.Services;

public class CreateOrderServiceTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<IMapper<Order, OrderRaisedEvent>> _orderMapperMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly Mock<IAlphaVantageService> _alphaVantageServiceMock;
    private readonly Mock<IGetInstrumentService> _getInstrumentServiceMock;
    private readonly Mock<IOrderBookManager> _orderBookManagerMock;
    private readonly Mock<IOrderBook> _orderBookMock;
    private readonly Mock<IProcessTradeService> _processTradeServiceMock;
    private readonly CreateOrderService _service;

    public CreateOrderServiceTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _orderMapperMock = new Mock<IMapper<Order, OrderRaisedEvent>>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();
        _alphaVantageServiceMock = new Mock<IAlphaVantageService>();
        _getInstrumentServiceMock = new Mock<IGetInstrumentService>();
        _orderBookManagerMock = new Mock<IOrderBookManager>();
        _orderBookMock = new Mock<IOrderBook>();
        _processTradeServiceMock = new Mock<IProcessTradeService>();

        _service = new CreateOrderService(
            _repositoryMock.Object,
            _orderMapperMock.Object,
            _diagnosticContextMock.Object,
            _alphaVantageServiceMock.Object,
            _getInstrumentServiceMock.Object,
            _orderBookManagerMock.Object,
            _processTradeServiceMock.Object
        );
    }

    [Fact]
    public async Task CreateOrder_ReturnsOrder_WhenOrderIsSuccessfullyCreated()
    {
        // Arrange
        var instrument = new Instrument(Guid.NewGuid(), "NEW", "NewInstrument", "TEST", Guid.NewGuid());
        var order = new Order(5, instrument.Id, 0.0, TradeSide.buy);
        var orderEvent = new OrderRaisedEvent(order.Id.ToString(), instrument.Id.ToString(), order.InitialQuantity,
            order.Price, order.Value, order.Status.ToString());
        var trades = new List<Trade>();

        _getInstrumentServiceMock
            .Setup(service => service.GetInstrument(order.InstrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT0(instrument));

        _orderMapperMock
            .Setup(mapper => mapper.Map(order))
            .Returns(orderEvent);

        _repositoryMock
            .Setup(repo => repo.InsertAsync(order, orderEvent))
            .ReturnsAsync((Error)null);

        _orderBookMock.Setup(orderBook => orderBook.AddOrder(order))
            .Returns(trades);

        _orderBookManagerMock
            .Setup(manager => manager.GetOrderBook(order.InstrumentId))
            .Returns(_orderBookMock.Object);

        _processTradeServiceMock
            .Setup(service => service.ProcessTrades(trades))
            .ReturnsAsync((Error)null);

        // Act
        var result = await _service.CreateOrder(order);

        // Assert
        var createdOrder = result.AsT0;
        Assert.Equal(order.Id, createdOrder.Id);
        Assert.Equal(order.Price, createdOrder.Price);
        _repositoryMock.Verify(repo => repo.InsertAsync(order, orderEvent), Times.Once);
        _diagnosticContextMock.Verify(dc => dc.Set("Order", order, true), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_ReturnsError_WhenInstrumentNotFound()
    {
        // Arrange
        var order = new Order(5, Guid.NewGuid(), 0.0, TradeSide.buy);
        var error = new Error(HttpStatusCode.InternalServerError, "Instrument not found");

        _getInstrumentServiceMock
            .Setup(service => service.GetInstrument(order.InstrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT1(error));

        // Act
        var result = await _service.CreateOrder(order);

        // Assert
        var errorResult = result.AsT1;
        Assert.Contains("Instrument not found", errorResult.ErrorCodes);
        _repositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Order>(), It.IsAny<OrderRaisedEvent>()), Times.Never);
    }

    [Fact]
    public async Task CreateOrder_ReturnsError_WhenAlphaVantageFails()
    {
        // Arrange
        var instrument = new Instrument(Guid.NewGuid(), "NEW", "NewInstrument", "TEST", Guid.NewGuid());
        var order = new Order(5, instrument.Id, -5.0, TradeSide.buy);
        var error = new Error(HttpStatusCode.InternalServerError, "AlphaVantage service failed");

        _getInstrumentServiceMock
            .Setup(service => service.GetInstrument(order.InstrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT0(instrument));

        _alphaVantageServiceMock
            .Setup(service => service.GetStockQuote(instrument.Ticker))
            .ReturnsAsync(OneOf<GlobalQuote, Error>.FromT1(error));

        // Act
        var result = await _service.CreateOrder(order);

        // Assert
        var errorResult = result.AsT1;
        Assert.Contains("AlphaVantage service failed", errorResult.ErrorCodes);
        _repositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Order>(), It.IsAny<OrderRaisedEvent>()), Times.Never);
    }
}