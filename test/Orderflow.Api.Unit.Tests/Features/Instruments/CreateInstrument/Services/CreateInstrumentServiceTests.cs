using System.Net;
using Moq;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.Common.Repositories;
using Orderflow.Features.Instruments.CreateInstrument.Events;
using Orderflow.Features.Instruments.CreateInstrument.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Instruments.CreateInstrument.Services;

public class CreateInstrumentServiceTests
{
    private readonly Mock<IInstrumentRepository> _repositoryMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly Mock<IMapper<Instrument, InstrumentCreatedEvent>> _mapperMock;
    private readonly CreateInstrumentService _service;

    public CreateInstrumentServiceTests()
    {
        _repositoryMock = new Mock<IInstrumentRepository>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();
        _mapperMock = new Mock<IMapper<Instrument, InstrumentCreatedEvent>>();

        _service = new CreateInstrumentService(
            _repositoryMock.Object,
            _diagnosticContextMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task CreateInstrument_ReturnsInstrument_WhenCreationIsSuccessful()
    {
        // Arrange
        var instrument = new Instrument(Guid.NewGuid(), "NEW", "NewInstrument", "TEST", Guid.NewGuid());
        var @event = new InstrumentCreatedEvent(instrument.Id.ToString(), instrument.Ticker, instrument.Name,
            instrument.ExchangeId.ToString(), instrument.Sector);

        _mapperMock
            .Setup(mapper => mapper.Map(It.IsAny<Instrument>()))
            .Returns(@event);

        _repositoryMock
            .Setup(repo => repo.InsertAsync(It.IsAny<Instrument>(), It.IsAny<InstrumentCreatedEvent>()))
            .ReturnsAsync((Error)null); // No error, creation is successful.

        // Act
        var result = await _service.CreateInstrument(instrument);

        // Assert
        var createdInstrument = result.AsT0;
        Assert.Equal(instrument.Name, createdInstrument.Name);
    }

    [Fact]
    public async Task CreateInstrument_ReturnsError_WhenCreationFails()
    {
        // Arrange
        var instrument = new Instrument(Guid.NewGuid(), "NEW", "NewInstrument", "TEST", Guid.NewGuid());
        var @event = new InstrumentCreatedEvent(instrument.Id.ToString(), instrument.Ticker, instrument.Name,
            instrument.ExchangeId.ToString(), instrument.Sector);

        var error = new Error(HttpStatusCode.InternalServerError, "Creation failed");

        _mapperMock
            .Setup(mapper => mapper.Map(It.IsAny<Instrument>()))
            .Returns(@event);

        _repositoryMock
            .Setup(repo => repo.InsertAsync(It.IsAny<Instrument>(), It.IsAny<InstrumentCreatedEvent>()))
            .ReturnsAsync(error); // Simulate error during creation.

        // Act
        var result = await _service.CreateInstrument(instrument);

        // Assert
        var errorResult = result.AsT1;
        Assert.Equal(error.ErrorType, errorResult.ErrorType);
    }
}