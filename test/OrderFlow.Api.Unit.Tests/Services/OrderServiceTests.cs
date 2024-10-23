using System.Net;
using AutoFixture.Xunit2;
using Moq;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain;
using OrderFlow.Domain.Models;
using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Services;

namespace OrderFlow.Api.Unit.Tests.Services;

public class OrderServiceTests
{
    [Theory, AutoMoqData]
    public async void Should_RetrieveOrder_If_Present(
        [Frozen] Mock<IRepository<Order>> mockRepository,
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
    public async void Should_ReturnError_If_OrderNotFound(
        [Frozen] Mock<IRepository<Order>> mockRepository,
        OrderService sut,
        string id)
    {
        var expectedError = new Error(HttpStatusCode.UnprocessableEntity, ErrorCodes.OrderNotFound);

        mockRepository.Setup(x =>
                x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.RetrieveOrder(id);

        var retrievedError = result.AsT1;

        Assert.Equal(expectedError, retrievedError);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnOrders(
        [Frozen] Mock<IRepository<Order>> mockRepository,
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
    public async void Should_ReturnError_IfQuery_Fails(
        [Frozen] Mock<IRepository<Order>> mockRepository,
        OrderService sut)
    {
        var expectedError = new Error(HttpStatusCode.Conflict, ErrorCodes.OrderNotFound);

        mockRepository.Setup(x => x.QueryAsync())
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.RetrieveOrders();

        Assert.True(result.IsT1);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_CreateOrder_And_SaveTo_Repo(
        [Frozen] Mock<IRepository<Order>> mockRepository,
        [Frozen] Mock<IMapper<Order, OrderCreatedEvent>> mockOrderToOrderCreatedEventMapper,
        [Frozen] Mock<IMapper<BaseOrderEvent, Event>> mockOrderEventToEventMapper,
        [Frozen] Mock<IEnqueueService> mockEnqueueService,
        Order order,
        OrderCreatedEvent orderCreatedEvent,
        Event @event,
        OrderService sut)
    {
        mockRepository.Setup(x =>
                x.InsertAsync(order, default))
            .ReturnsAsync(order)
            .Verifiable();

        mockOrderToOrderCreatedEventMapper.Setup(x => x.Map(order))
            .Returns(orderCreatedEvent)
            .Verifiable();

        mockOrderEventToEventMapper.Setup(x => x.Map(orderCreatedEvent))
            .Returns(@event)
            .Verifiable();

        mockEnqueueService.Setup(x => x.PublishEvent(@event))
            .ReturnsAsync(true)
            .Verifiable();

        var result = await sut.CreateOrder(order);

        var createdOrder = result.AsT0;

        Assert.Equal(order, createdOrder);

        mockRepository.Verify();
        mockOrderToOrderCreatedEventMapper.Verify();
        mockOrderEventToEventMapper.Verify();
        mockEnqueueService.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_If_Repo_Fails(
        [Frozen] Mock<IRepository<Order>> mockRepository,
        Order order,
        OrderService sut)
    {
        var expectedError = new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderCouldNotBeCreated);

        mockRepository.Setup(x =>
                x.InsertAsync(order, default))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.CreateOrder(order);

        var error = result.AsT1;

        Assert.Equal(expectedError, error);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_If_EnqueueService_Fails_To_Publish(
        [Frozen] Mock<IRepository<Order>> mockRepository,
        [Frozen] Mock<IMapper<Order, OrderCreatedEvent>> mockOrderToOrderCreatedEventMapper,
        [Frozen] Mock<IMapper<BaseOrderEvent, Event>> mockOrderEventToEventMapper,
        [Frozen] Mock<IEnqueueService> mockEnqueueService,
        Order order,
        OrderCreatedEvent orderCreatedEvent,
        Event @event,
        OrderService sut)
    {
        mockRepository.Setup(x =>
                x.InsertAsync(order, default))
            .ReturnsAsync(order)
            .Verifiable();

        mockOrderToOrderCreatedEventMapper.Setup(x => x.Map(order))
            .Returns(orderCreatedEvent)
            .Verifiable();

        mockOrderEventToEventMapper.Setup(x => x.Map(orderCreatedEvent))
            .Returns(@event)
            .Verifiable();

        mockEnqueueService.Setup(x => x.PublishEvent(@event))
            .ReturnsAsync(false)
            .Verifiable();

        var expectedError = new Error(HttpStatusCode.InternalServerError, ErrorCodes.EventCouldNotBePublished);

        var result = await sut.CreateOrder(order);

        var error = result.AsT1;

        Assert.Equivalent(expectedError, error);

        mockRepository.Verify();
        mockOrderToOrderCreatedEventMapper.Verify();
        mockOrderEventToEventMapper.Verify();
        mockEnqueueService.Verify();
    }
}