using System.Net;
using AutoFixture.Xunit2;
using Moq;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Mappers;
using Orderflow.Services;

namespace Orderflow.Api.Unit.Tests.Services;

public class InstrumentServiceTests
{
    [Theory, AutoMoqData]
    public async Task Should_RetrieveInstrument_If_Present(
        [Frozen] Mock<IInstrumentRepository> mockRepository,
        Instrument instrument,
        InstrumentService sut)
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
    public async Task Should_ReturnError_If_InstrumentNotFound(
        [Frozen] Mock<IInstrumentRepository> mockRepository,
        InstrumentService sut,
        string id)
    {
        var expectedError = new Error(HttpStatusCode.UnprocessableEntity, ErrorCodes.InstrumentNotFound);

        mockRepository.Setup(x =>
                x.GetByIdAsync(id))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.RetrieveInstrument(id);

        var retrievedError = result.AsT1;

        Assert.Equal(expectedError, retrievedError);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnInstruments(
        [Frozen] Mock<IInstrumentRepository> mockRepository,
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
    public async Task Should_ReturnError_IfQuery_Fails(
        [Frozen] Mock<IInstrumentRepository> mockRepository,
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

    [Theory, AutoMoqData]
    public async Task Should_CreateInstrument_And_SaveTo_Repo(
        [Frozen] Mock<IInstrumentRepository> mockRepository,
        [Frozen] Mock<IMapper<Instrument, InstrumentCreatedEvent>> instrumentToInstrumentCreatedEventMapperMock,
        Instrument instrument,
        InstrumentCreatedEvent @event,
        InstrumentService sut)
    {
        instrumentToInstrumentCreatedEventMapperMock
            .Setup(x => x.Map(instrument))
            .Returns(@event)
            .Verifiable();

        mockRepository
            .Setup(x => x.InsertAsync(instrument, @event))
            .ReturnsAsync((Error?)null)
            .Verifiable();

        var result = await sut.CreateInstrument(instrument);

        var createdInstrument = result.AsT0;

        Assert.Equal(instrument, createdInstrument);

        mockRepository.Verify();
        instrumentToInstrumentCreatedEventMapperMock.Verify();
    }

    [Theory, AutoMoqData]
    public async Task Should_ReturnError_If_Repo_Insert_Fails(
        [Frozen] Mock<IInstrumentRepository> mockRepository,
        [Frozen] Mock<IMapper<Instrument, InstrumentCreatedEvent>> instrumentToInstrumentCreatedEventMapperMock,
        Instrument instrument,
        InstrumentCreatedEvent @event,
        Error error,
        InstrumentService sut)
    {
        instrumentToInstrumentCreatedEventMapperMock
            .Setup(x => x.Map(instrument))
            .Returns(@event)
            .Verifiable();

        mockRepository.Setup(x =>
                x.InsertAsync(instrument, @event))
            .ReturnsAsync(error)
            .Verifiable();

        var result = await sut.CreateInstrument(instrument);

        Assert.Equal(error, result.AsT1);

        mockRepository.Verify();
        instrumentToInstrumentCreatedEventMapperMock.Verify();
    }
}