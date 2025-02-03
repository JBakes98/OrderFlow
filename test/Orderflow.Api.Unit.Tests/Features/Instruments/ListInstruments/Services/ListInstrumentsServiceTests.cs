using System.Net;
using Moq;
using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.Common.Repositories;
using Orderflow.Features.Instruments.ListInstruments.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Instruments.ListInstruments.Services;

public class ListInstrumentsServiceTests
{
    private readonly Mock<IInstrumentRepository> _repositoryMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly ListInstrumentsService _service;

    public ListInstrumentsServiceTests()
    {
        _repositoryMock = new Mock<IInstrumentRepository>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();

        _service = new ListInstrumentsService(
            _repositoryMock.Object,
            _diagnosticContextMock.Object
        );
    }

    [Fact]
    public async Task ListInstruments_ReturnsInstruments_WhenRepositoryReturnsSuccess()
    {
        // Arrange
        var instruments = new List<Instrument>
        {
            new(Guid.NewGuid(), "NEW1", "Instrument1", "TEST", Guid.NewGuid()),
            new(Guid.NewGuid(), "NEW2", "Instrument1", "TEST", Guid.NewGuid())
        };

        _repositoryMock
            .Setup(repo => repo.QueryAsync())
            .ReturnsAsync(OneOf<IEnumerable<Instrument>, Error>.FromT0(instruments));

        // Act
        var result = await _service.ListInstruments();

        // Assert
        var retrievedInstruments = result.AsT0.ToList();
        Assert.NotNull(retrievedInstruments);
        Assert.Equal(2, retrievedInstruments.Count());
        Assert.Equivalent(retrievedInstruments, instruments);
    }

    [Fact]
    public async Task ListInstruments_ReturnsError_WhenRepositoryReturnsError()
    {
        // Arrange
        var error = new Error(HttpStatusCode.InternalServerError, "Failed to retrieve instruments");

        _repositoryMock
            .Setup(repo => repo.QueryAsync())
            .ReturnsAsync(OneOf<IEnumerable<Instrument>, Error>.FromT1(error));

        // Act
        var result = await _service.ListInstruments();

        // Assert
        var errorResult = result.AsT1;
        Assert.Contains("Failed to retrieve instruments", errorResult.ErrorCodes);
    }
}