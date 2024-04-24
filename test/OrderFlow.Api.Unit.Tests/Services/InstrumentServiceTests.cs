using System.Net;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Models;
using OrderFlow.Repositories;
using OrderFlow.Services;

namespace OrderFlow.Api.Unit.Tests.Services;

public class InstrumentServiceTests
{
    [Theory, AutoMoqData]
    public async void Should_RetrieveInstrument_If_Present(
        [Frozen] Mock<IRepository<Instrument>> mockRepository,
        Instrument instrument,
        InstrumentService sut,
        IFixture fixture)
    {
        mockRepository.Setup(x =>
                x.GetByIdAsync(instrument.Id))
            .ReturnsAsync(instrument)
            .Verifiable();

        var result = await sut.RetrieveInstrument(instrument.Id);

        var retrievedInstrument = result.AsT0;

        Assert.Equal(instrument, retrievedInstrument);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_If_InstrumentNotFound(
        [Frozen] Mock<IRepository<Instrument>> mockRepository,
        InstrumentService sut,
        string id,
        IFixture fixture)
    {
        var expectedError = new Error(HttpStatusCode.UnprocessableEntity, ErrorCodes.InstrumentNotFound);

        mockRepository.Setup(x =>
                x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.RetrieveInstrument(id);

        var retrievedError = result.AsT1;

        Assert.Equal(expectedError, retrievedError);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnInstruments(
        [Frozen] Mock<IRepository<Instrument>> mockRepository,
        InstrumentService sut,
        List<Instrument> instruments)
    {
        mockRepository.Setup(x => x.QueryAsync())
            .ReturnsAsync(instruments)
            .Verifiable();

        var result = await sut.RetrieveInstruments();

        Assert.True(result.IsT0);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_IfQuery_Fails(
        [Frozen] Mock<IRepository<Instrument>> mockRepository,
        InstrumentService sut)
    {
        var expectedError = new Error(HttpStatusCode.Conflict, ErrorCodes.InstrumentNotFound);

        mockRepository.Setup(x => x.QueryAsync())
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.RetrieveInstruments();

        Assert.True(result.IsT1);
        mockRepository.Verify();
    }
}