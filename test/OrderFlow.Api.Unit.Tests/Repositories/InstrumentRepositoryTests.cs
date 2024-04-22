using System.Net;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture.Xunit2;
using Moq;
using OrderFlow.Domain;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Api.Unit.Tests.Repositories;

public class InstrumentRepositoryTests
{
    [Theory, AutoMoqData]
    public async void Repository_GetByIdAsync_Should_ReturnObject(
        Instrument instrument,
        [Frozen] Mock<IDynamoDBContext> mockContext,
        InstrumentRepository sut)
    {
        mockContext.Setup(x => x.LoadAsync<Instrument>(instrument.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(instrument)
            .Verifiable();

        var result = await sut.GetByIdAsync(instrument.Id);

        var resultInstrument = result.AsT0;
        Assert.Equal(instrument, resultInstrument);
        mockContext.Verify();
    }

    [Theory, AutoMoqData]
    public async void Repository_GetByIdAsync_Should_ReturnError(
        [Frozen] Mock<IDynamoDBContext> mockContext,
        string id,
        InstrumentRepository sut)
    {
        mockContext.Setup(x => x.LoadAsync<Instrument>(id, It.IsAny<CancellationToken>()))!
            .ReturnsAsync(null as Instrument)
            .Verifiable();

        var expected = new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        var result = await sut.GetByIdAsync(id);

        var resultError = result.AsT1;
        Assert.Equivalent(expected, resultError);
        mockContext.Verify();
    }
    
    [Theory, AutoMoqData]
    public async void Repository_QueryAsync_Should_ReturnObjects(
        List<Instrument> instruments,
        [Frozen] Mock<IDynamoDBContext> mockContext,
        InstrumentRepository sut)
    {
        mockContext.Setup(
                x => x.ScanAsync<Instrument>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>())
                    .GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(instruments)
            .Verifiable();
        var result = await sut.QueryAsync();

        Assert.True(result.IsT0);
        mockContext.Verify();
    }
    
    [Theory, AutoMoqData]
    public async void Repository_QueryAsync_Should_ReturnError(
        List<Instrument> instruments,
        [Frozen] Mock<IDynamoDBContext> mockContext,
        InstrumentRepository sut)
    {
        mockContext.Setup(
                x => x.ScanAsync<Instrument>(It.IsAny<List<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>())
                    .GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(instruments)
            .Verifiable();
        var result = await sut.QueryAsync();

        Assert.True(result.IsT0);
        mockContext.Verify();
    }
}