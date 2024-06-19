using System.Net;
using AutoFixture.Xunit2;
using Moq;
using OrderFlow.Domain;
using OrderFlow.Models;
using OrderFlow.Repositories;
using OrderFlow.Services;

namespace OrderFlow.Api.Unit.Tests.Services;

public class OrderServiceTests
{
    [Theory, AutoMoqData]
    public async void Should_RetrieveOrder_If_Present(
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
    public async void Should_ReturnError_If_OrderNotFound(
        [Frozen] Mock<IOrderRepository> mockRepository,
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
    public async void Should_ReturnError_IfQuery_Fails(
        [Frozen] Mock<IOrderRepository> mockRepository,
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
}