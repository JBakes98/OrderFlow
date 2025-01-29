using System.Net;
using Moq;
using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.Common.Repositories;
using Orderflow.Features.Instruments.GetInstrument.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Instruments.GetInstrument.Services;

public class GetInstrumentServiceTests
{
    private readonly Mock<IInstrumentRepository> _repositoryMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly GetInstrumentService _service;

    public GetInstrumentServiceTests()
    {
        _repositoryMock = new Mock<IInstrumentRepository>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();

        _service = new GetInstrumentService(
            _repositoryMock.Object,
            _diagnosticContextMock.Object
        );
    }

    [Fact]
    public async Task GetInstrument_ReturnsInstrument_WhenRepositoryReturnsSuccess()
    {
        // Arrange
        var instrumentId = Guid.NewGuid();
        var instrument = new Instrument(instrumentId, "NEW", "NewInstrument", "TEST", Guid.NewGuid());

        _repositoryMock
            .Setup(repo => repo.GetByIdAsync(instrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT0(instrument));

        // Act
        var result = await _service.GetInstrument(instrumentId);

        // Assert
        var retrievedInstrument = result.AsT0;
        Assert.Equal(instrumentId, retrievedInstrument.Id);
        Assert.Equal(instrument.Name, retrievedInstrument.Name);
        _diagnosticContextMock.Verify(dc => dc.Set("InstrumentEntity", instrument, true), Times.Once);
    }

    [Fact]
    public async Task GetInstrument_ReturnsError_WhenRepositoryReturnsError()
    {
        // Arrange
        var instrumentId = Guid.NewGuid();
        var error = new Error(HttpStatusCode.InternalServerError, "Instrument not found");

        _repositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT1(error));

        // Act
        var result = await _service.GetInstrument(instrumentId);

        // Assert
        var errorResult = result.AsT1;
        Assert.Contains("Instrument not found", errorResult.ErrorCodes);
        _diagnosticContextMock.Verify(dc => dc.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()),
            Times.Never);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetInstrumentService(null, _diagnosticContextMock.Object));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDiagnosticContextIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetInstrumentService(_repositoryMock.Object, null));
    }
}