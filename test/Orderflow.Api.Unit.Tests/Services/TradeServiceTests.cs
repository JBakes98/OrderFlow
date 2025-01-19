using System.Net;
using Moq;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Trade;
using Orderflow.Mappers;
using Orderflow.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Services;

public class TradeServiceTests
{
    private readonly Mock<ITradeRepository> _repositoryMock;
    private readonly Mock<IMapper<Trade, TradeExecutedEvent>> _mapperMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly TradeService _tradeService;

    public TradeServiceTests()
    {
        _repositoryMock = new Mock<ITradeRepository>();
        _mapperMock = new Mock<IMapper<Trade, TradeExecutedEvent>>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();

        _tradeService = new TradeService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _diagnosticContextMock.Object
        );
    }

    [Fact]
    public async Task ProcessTrades_ShouldProcessTradesAndMapEventsAndStoreTrade()
    {
        // Arrange
        var trades = new List<Trade>
        {
            new(Guid.NewGuid(), Guid.NewGuid(), 100, 10),
            new(Guid.NewGuid(), Guid.NewGuid(), 200, 20)
        };

        var tradeExecutedEvents = trades.Select(t => new TradeExecutedEvent(
            t.Id.ToString(),
            t.BuyOrderId.ToString(),
            t.SellOrderId.ToString(),
            t.Price,
            t.Quantity,
            t.Value,
            t.Timestamp
        )).ToList();

        _mapperMock
            .Setup(m => m.Map(It.IsAny<Trade>()))
            .Returns((Trade t) => new TradeExecutedEvent(
                t.Id.ToString(),
                t.BuyOrderId.ToString(),
                t.SellOrderId.ToString(),
                t.Price,
                t.Quantity,
                t.Value,
                t.Timestamp
            ));

        _repositoryMock
            .Setup(r => r.InsertAsync(trades, tradeExecutedEvents))
            .ReturnsAsync((Error?)null);

        // Act
        var result = await _tradeService.ProcessTrades(trades);

        // Assert
        Assert.Null(result);
        _mapperMock.Verify(m => m.Map(It.IsAny<Trade>()), Times.Exactly(trades.Count));
        _repositoryMock.Verify(r => r.InsertAsync(trades, It.IsAny<List<TradeExecutedEvent>>()), Times.Once);
        _diagnosticContextMock.Verify(d => d.Set("TradeExecuted", true, false), Times.Once);
    }

    [Fact]
    public async Task ProcessTrades_ShouldReturnError_WhenRepositoryFails()
    {
        // Arrange
        var trades = new List<Trade>
        {
            new Trade(Guid.NewGuid(), Guid.NewGuid(), 100, 10)
        };

        var error = new Error(HttpStatusCode.InternalServerError, "Database insert failed");

        _repositoryMock
            .Setup(r => r.InsertAsync(trades, It.IsAny<List<TradeExecutedEvent>>()))
            .ReturnsAsync(error);

        // Act
        var result = await _tradeService.ProcessTrades(trades);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(error.ErrorCodes, result.ErrorCodes);
        _diagnosticContextMock.Verify(d => d.Set("TradeExecuted", false, false), Times.Never);
    }

    [Fact]
    public async Task ProcessTrades_ShouldHandleEmptyTradeList()
    {
        // Arrange
        var trades = new List<Trade>();

        _repositoryMock
            .Setup(r => r.InsertAsync(trades, It.IsAny<List<TradeExecutedEvent>>()))
            .ReturnsAsync((Error?)null);

        // Act
        var result = await _tradeService.ProcessTrades(trades);

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(r => r.InsertAsync(trades, It.IsAny<List<TradeExecutedEvent>>()), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<Trade>()), Times.Never);
        _diagnosticContextMock.Verify(d => d.Set("TradeExecuted", false, false), Times.Never);
    }
}