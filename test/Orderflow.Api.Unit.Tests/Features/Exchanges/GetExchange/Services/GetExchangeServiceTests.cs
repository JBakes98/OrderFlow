using System.Net;
using Moq;
using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.Common.Repositories;
using Orderflow.Features.Exchanges.GetExchange.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.GetExchange.Services;

public class GetExchangeServiceTests
{
    private readonly Mock<IExchangeRepository> _repositoryMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly GetExchangeService _service;

    public GetExchangeServiceTests()
    {
        _repositoryMock = new Mock<IExchangeRepository>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();

        _service = new GetExchangeService(
            _repositoryMock.Object,
            _diagnosticContextMock.Object);
    }

    [Fact]
    public async Task GetExchangeById_ShouldReturnExchange_WhenExchangeExists()
    {
        // Arrange
        var exchangeId = Guid.NewGuid();
        var exchange = new Exchange(exchangeId, "Test Exchange", "TEST", "TST", "US");

        _repositoryMock.Setup(r => r.GetByIdAsync(exchangeId))
            .ReturnsAsync(OneOf<Exchange, Error>.FromT0(exchange));

        // Act
        var result = await _service.GetExchangeById(exchangeId);

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(exchange, result.AsT0);
        _repositoryMock.Verify(r => r.GetByIdAsync(exchangeId), Times.Once);
        _diagnosticContextMock.Verify(d => d.Set("ExchangeEntity", exchange, true), Times.Once);
    }

    [Fact]
    public async Task GetExchangeById_ShouldReturnError_WhenExchangeDoesNotExist()
    {
        // Arrange
        var exchangeId = Guid.NewGuid();
        var error = new Error(HttpStatusCode.InternalServerError, "Exchange not found");

        _repositoryMock.Setup(r => r.GetByIdAsync(exchangeId))
            .ReturnsAsync(OneOf<Exchange, Error>.FromT1(error));

        // Act
        var result = await _service.GetExchangeById(exchangeId);

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);
        _repositoryMock.Verify(r => r.GetByIdAsync(exchangeId), Times.Once);
        _diagnosticContextMock.Verify(d => d.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()),
            Times.Never);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetExchangeService(null, _diagnosticContextMock.Object));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDiagnosticContextIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new GetExchangeService(_repositoryMock.Object, null));
    }
}