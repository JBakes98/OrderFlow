using System.Net;
using AutoFixture.Xunit2;
using Moq;
using OneOf;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Mappers;
using Orderflow.Services;
using Orderflow.Services.AlphaVantage;

namespace Orderflow.Api.Unit.Tests.Services;

public class OrderServiceTests
{
    [Theory, AutoMoqData]
    public async Task Should_RetrieveOrder_If_Present(
        [Frozen] Mock<IOrderRepository> mockRepository,
        Order order,
        OrderService sut)
    {
        mockRepository.Setup(x =>
                x.GetByIdAsync(order.Id))
            .ReturnsAsync(order)
            .Verifiable();

        var result = await sut.RetrieveOrder(order.Id);

        var retrievedOrder = result.AsT0;

        Assert.Equal(order, retrievedOrder);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnError_If_OrderNotFound(
        [Frozen] Mock<IOrderRepository> mockRepository,
        OrderService sut,
        string id,
        Error error)
    {
        mockRepository.Setup(x =>
                x.GetByIdAsync(id))
            .ReturnsAsync(error)
            .Verifiable();

        var result = await sut.RetrieveOrder(id);

        var retrievedError = result.AsT1;

        Assert.Equal(error, retrievedError);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnOrders(
        [Frozen] Mock<IOrderRepository> mockRepository,
        OrderService sut,
        List<Order> orders)
    {
        mockRepository.Setup(x => x.QueryAsync())
            .ReturnsAsync(orders)
            .Verifiable();

        var result = await sut.RetrieveOrders();

        Assert.True(result.IsT0);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnError_IfQuery_Fails(
        [Frozen] Mock<IOrderRepository> mockRepository,
        Error error,
        OrderService sut)
    {
        mockRepository.Setup(x => x.QueryAsync())
            .ReturnsAsync(error)
            .Verifiable();

        var result = await sut.RetrieveOrders();

        Assert.True(result.IsT1);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_CreateOrder_And_SaveTo_Repo(
        [Frozen] Mock<IOrderRepository> mockRepository,
        [Frozen] Mock<IMapper<Order, OrderRaisedEvent>> mockOrderToOrderRaisedEventMapper,
        [Frozen] Mock<IInstrumentService> mockInstrumentService,
        [Frozen] Mock<IAlphaVantageService> mockAlphaVantageService,
        Order order,
        Instrument instrument,
        GlobalQuote globalQuote,
        OrderRaisedEvent @event,
        OrderService sut)
    {
        mockInstrumentService
            .Setup(x => x.RetrieveInstrument(order.InstrumentId))
            .ReturnsAsync(instrument)
            .Verifiable();

        mockAlphaVantageService
            .Setup(x => x.GetStockQuote(instrument.Ticker))
            .ReturnsAsync(globalQuote)
            .Verifiable();

        mockOrderToOrderRaisedEventMapper
            .Setup(x => x.Map(order))
            .Returns(@event)
            .Verifiable();

        mockRepository.Setup(x =>
                x.InsertAsync(order, @event))
            .ReturnsAsync((Error?)null)
            .Verifiable();

        var result = await sut.CreateOrder(order);

        Assert.True(result.IsT0);
        Assert.Equal(order, result.AsT0);

        mockInstrumentService.Verify();
        mockAlphaVantageService.Verify();
        mockRepository.Verify();
        mockOrderToOrderRaisedEventMapper.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnError_If_Instrument_Not_Found(
        [Frozen] Mock<IInstrumentService> mockInstrumentService,
        OrderRaisedEvent @event,
        Order order,
        GlobalQuote globalQuote,
        OrderService sut)
    {
        var expectedError = new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        mockInstrumentService
            .Setup(x => x.RetrieveInstrument(order.InstrumentId))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.CreateOrder(order);

        Assert.True(result.IsT1);
        Assert.Equal(expectedError, result.AsT1);

        mockInstrumentService.Verify();
    }


    [Theory, AutoMoqData]
    public async Task? Should_ReturnError_If_AlphaVantage_Stock_Quote_Fails(
        [Frozen] Mock<IInstrumentService> mockInstrumentService,
        [Frozen] Mock<IAlphaVantageService> mockAlphaVantageService,
        OrderRaisedEvent @event,
        Order order,
        Instrument instrument,
        GlobalQuote globalQuote,
        OrderService sut)
    {
        var expectedError = new Error(HttpStatusCode.InternalServerError,
            ErrorCodes.UnableToRetrieveCurrentInstrumentPrice);

        mockInstrumentService
            .Setup(x => x.RetrieveInstrument(order.InstrumentId))
            .ReturnsAsync(instrument)
            .Verifiable();

        mockAlphaVantageService
            .Setup(x => x.GetStockQuote(instrument.Ticker))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.CreateOrder(order);

        Assert.True(result.IsT1);
        Assert.Equal(expectedError, result.AsT1);

        mockInstrumentService.Verify();
        mockAlphaVantageService.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnError_If_Repo_Fails(
        [Frozen] Mock<IInstrumentService> mockInstrumentService,
        [Frozen] Mock<IAlphaVantageService> mockAlphaVantageService,
        [Frozen] Mock<IOrderRepository> mockRepository,
        [Frozen] Mock<IMapper<Order, OrderRaisedEvent>> mockOrderToOrderRaisedEventMapper,
        OrderRaisedEvent @event,
        Order order,
        Instrument instrument,
        GlobalQuote globalQuote,
        OrderService sut)
    {
        mockInstrumentService
            .Setup(x => x.RetrieveInstrument(order.InstrumentId))
            .ReturnsAsync(instrument)
            .Verifiable();

        mockAlphaVantageService
            .Setup(x => x.GetStockQuote(instrument.Ticker))
            .ReturnsAsync(globalQuote)
            .Verifiable();

        var expectedError = new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderCouldNotBeCreated);

        mockOrderToOrderRaisedEventMapper
            .Setup(x => x.Map(order))
            .Returns(@event)
            .Verifiable();

        mockRepository.Setup(x =>
                x.InsertAsync(order, @event))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.CreateOrder(order);

        Assert.True(result.IsT1);
        Assert.Equal(expectedError, result.AsT1);

        mockInstrumentService.Verify();
        mockAlphaVantageService.Verify();
        mockRepository.Verify();
        mockOrderToOrderRaisedEventMapper.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_return_orders_for_a_specific_instrument(
        [Frozen] Mock<IOrderRepository> mockRepository,
        Instrument instrument,
        List<Order> orders,
        OrderService sut)
    {
        mockRepository.Setup(x => x.GetInstrumentOrders(instrument.Id))
            .ReturnsAsync(OneOf<IEnumerable<Order>, Error>.FromT0(orders))
            .Verifiable();

        var result = await sut.RetrieveInstrumentOrders(instrument.Id);

        Assert.Equal(orders, result.AsT0);
        mockRepository.Verify();
    }
}