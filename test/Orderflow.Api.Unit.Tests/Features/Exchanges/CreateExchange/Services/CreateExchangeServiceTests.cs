using System.Net;
using Moq;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.Common.Repositories;
using Orderflow.Features.Exchanges.CreateExchange.Events;
using Orderflow.Features.Exchanges.CreateExchange.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.CreateExchange.Services;

public class CreateExchangeServiceTests
{
    private readonly Mock<IExchangeRepository> _repositoryMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly Mock<IMapper<Exchange, ExchangeCreatedEvent>> _mapperMock;
    private readonly CreateExchangeService _service;

    public CreateExchangeServiceTests()
    {
        _repositoryMock = new Mock<IExchangeRepository>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();
        _mapperMock = new Mock<IMapper<Exchange, ExchangeCreatedEvent>>();

        _service = new CreateExchangeService(
            _repositoryMock.Object,
            _diagnosticContextMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task CreateExchange_ShouldReturnExchange_WhenInsertSucceeds()
    {
        // Arrange
        var exchange = new Exchange(Guid.NewGuid(), "Test Exchange", "TEST", "TST", "US");
        var exchangeEvent = new ExchangeCreatedEvent(Guid.NewGuid().ToString(), "Test Exchange", "TEST", "TST", "US");
        ;

        _mapperMock.Setup(m => m.Map(exchange)).Returns(exchangeEvent);
        _repositoryMock.Setup(r => r.InsertAsync(exchange, exchangeEvent)).ReturnsAsync((Error)null);

        // Act
        var result = await _service.CreateExchange(exchange);

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(exchange, result.AsT0);

        _mapperMock.Verify(m => m.Map(exchange), Times.Once);
        _repositoryMock.Verify(r => r.InsertAsync(exchange, exchangeEvent), Times.Once);
    }

    [Fact]
    public async Task CreateExchange_ShouldReturnError_WhenInsertFails()
    {
        // Arrange
        var exchange = new Exchange(Guid.NewGuid(), "Test Exchange", "TEST", "TST", "US");
        var exchangeEvent = new ExchangeCreatedEvent(Guid.NewGuid().ToString(), "Test Exchange", "TEST", "TST", "US");
        var error = new Error(HttpStatusCode.InternalServerError, "Insertion failed");

        _mapperMock.Setup(m => m.Map(exchange)).Returns(exchangeEvent);
        _repositoryMock.Setup(r => r.InsertAsync(exchange, exchangeEvent)).ReturnsAsync(error);

        // Act
        var result = await _service.CreateExchange(exchange);

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);

        _mapperMock.Verify(m => m.Map(exchange), Times.Once);
        _repositoryMock.Verify(r => r.InsertAsync(exchange, exchangeEvent), Times.Once);
    }
}