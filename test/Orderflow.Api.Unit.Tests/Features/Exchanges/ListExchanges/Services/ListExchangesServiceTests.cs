using System.Net;
using Moq;
using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.Common.Repositories;
using Orderflow.Features.Exchanges.ListExchanges.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.ListExchanges.Services;

public class ListExchangeServiceTests
{
    private readonly Mock<IExchangeRepository> _repositoryMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly ListExchangesService _service;

    public ListExchangeServiceTests()
    {
        _repositoryMock = new Mock<IExchangeRepository>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();

        _service = new ListExchangesService(
            _repositoryMock.Object,
            _diagnosticContextMock.Object);
    }

    [Fact]
    public async Task ListExchanges_ShouldReturnExchanges_WhenExchangesExist()
    {
        // Arrange
        var exchanges = new List<Exchange>
        {
            new Exchange(Guid.NewGuid(), "Exchange1", "EX1", "MIC1", "Region1"),
            new Exchange(Guid.NewGuid(), "Exchange2", "EX2", "MIC2", "Region2")
        };

        _repositoryMock.Setup(r => r.QueryAsync())
            .ReturnsAsync(OneOf<IEnumerable<Exchange>, Error>.FromT0(exchanges));

        // Act
        var result = await _service.ListExchanges();

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(exchanges, result.AsT0);
        _repositoryMock.Verify(r => r.QueryAsync(), Times.Once);
    }

    [Fact]
    public async Task ListExchanges_ShouldReturnError_WhenRepositoryReturnsError()
    {
        // Arrange
        var error = new Error(HttpStatusCode.InternalServerError, "Query failed");

        _repositoryMock.Setup(r => r.QueryAsync())
            .ReturnsAsync(OneOf<IEnumerable<Exchange>, Error>.FromT1(error));

        // Act
        var result = await _service.ListExchanges();

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);
        _repositoryMock.Verify(r => r.QueryAsync(), Times.Once);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ListExchangesService(null, _diagnosticContextMock.Object));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDiagnosticContextIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ListExchangesService(_repositoryMock.Object, null));
    }
}