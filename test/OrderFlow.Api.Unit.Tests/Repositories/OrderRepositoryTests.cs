using System.Net;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture.Xunit2;
using Moq;
using OrderFlow.Domain;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Api.Unit.Tests.Repositories;

public class OrderRepositoryTests
{
    [Theory, AutoMoqData]
    public async void Repository_GetByIdAsync_Should_ReturnObject(
        Order order,
        [Frozen] Mock<IDynamoDBContext> mockContext,
        OrderRepository sut)
    {
        mockContext.Setup(x => x.LoadAsync<Order>(order.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order)
            .Verifiable();

        var result = await sut.GetByIdAsync(order.Id);

        var resultOrder = result.AsT0;
        Assert.Equal(order, resultOrder);
        mockContext.Verify();
    }

    [Theory, AutoMoqData]
    public async void Repository_GetByIdAsync_Should_ReturnError(
        [Frozen] Mock<IDynamoDBContext> mockContext,
        string id,
        OrderRepository sut)
    {
        mockContext.Setup(x => x.LoadAsync<Order>(id, It.IsAny<CancellationToken>()))!
            .ReturnsAsync(null as Order)
            .Verifiable();

        var expected = new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        var result = await sut.GetByIdAsync(id);

        var resultError = result.AsT1;
        Assert.Equivalent(expected, resultError);
        mockContext.Verify();
    }

    [Theory, AutoMoqData]
    public async void Repository_QueryAsync_Should_ReturnObjects(
        List<Order> orders,
        [Frozen] Mock<IDynamoDBContext> mockContext,
        OrderRepository sut)
    {
        mockContext.Setup(
                x => x.ScanAsync<Order>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>())
                    .GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders)
            .Verifiable();

        var result = await sut.QueryAsync();

        Assert.True(result.IsT0);
        mockContext.Verify();
    }

    [Theory, AutoMoqData]
    public async void Repository_QueryAsync_Should_ReturnError(
        List<Order> orders,
        [Frozen] Mock<IDynamoDBContext> mockContext,
        OrderRepository sut)
    {
        mockContext.Setup(
                x => x.ScanAsync<Order>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>())
                    .GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders)
            .Verifiable();
        var result = await sut.QueryAsync();

        Assert.True(result.IsT0);
        mockContext.Verify();
    }
}