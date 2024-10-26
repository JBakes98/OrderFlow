using System.Net;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture.Xunit2;
using Moq;
using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Data.Entities;
using OrderFlow.Data.Repositories;
using OrderFlow.Domain;
using OrderFlow.Domain.Models;

namespace OrderFlow.Api.Unit.Tests.Repositories;

public class OrderRepositoryTests
{
    // [Theory, AutoMoqData]
    // public async void Repository_GetByIdAsync_Should_ReturnObject(
    //     OrderEntity orderEntity,
    //     [Frozen] Mock<IDynamoDBContext> mockContext,
    //     OrderRepository sut)
    // {
    //     mockContext.Setup(x => x.LoadAsync<OrderEntity>(orderEntity.Id, It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(orderEntity)
    //         .Verifiable();
    //
    //     var result = await sut.GetByIdAsync(orderEntity.Id);
    //
    //     var resultOrder = result.AsT0;
    //     Assert.Equal(orderEntity, resultOrder);
    //     mockContext.Verify();
    // }
    //
    // [Theory, AutoMoqData]
    // public async void Repository_GetByIdAsync_Should_ReturnError(
    //     [Frozen] Mock<IDynamoDBContext> mockContext,
    //     string id,
    //     OrderRepository sut)
    // {
    //     mockContext.Setup(x => x.LoadAsync<OrderEntity>(id, It.IsAny<CancellationToken>()))!
    //         .ReturnsAsync(null as OrderEntity)
    //         .Verifiable();
    //
    //     var expected = new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);
    //
    //     var result = await sut.GetByIdAsync(id);
    //
    //     var resultError = result.AsT1;
    //     Assert.Equivalent(expected, resultError);
    //     mockContext.Verify();
    // }
    //
    // [Theory, AutoMoqData]
    // public async void Repository_QueryAsync_Should_ReturnObjects(
    //     List<OrderEntity> orders,
    //     [Frozen] Mock<IDynamoDBContext> mockContext,
    //     OrderRepository sut)
    // {
    //     mockContext.Setup(
    //             x => x.ScanAsync<OrderEntity>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>())
    //                 .GetRemainingAsync(It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(orders)
    //         .Verifiable();
    //
    //     var result = await sut.QueryAsync();
    //
    //     Assert.True(result.IsT0);
    //     mockContext.Verify();
    // }
    //
    // [Theory, AutoMoqData]
    // public async void Repository_QueryAsync_Should_ReturnError(
    //     List<OrderEntity> orders,
    //     [Frozen] Mock<IDynamoDBContext> mockContext,
    //     OrderRepository sut)
    // {
    //     mockContext.Setup(
    //             x => x.ScanAsync<OrderEntity>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>())
    //                 .GetRemainingAsync(It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(orders)
    //         .Verifiable();
    //     var result = await sut.QueryAsync();
    //
    //     Assert.True(result.IsT0);
    //     mockContext.Verify();
    // }
}