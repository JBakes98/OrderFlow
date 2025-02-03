using System.Net;
using Moq;
using Orderflow.Common.Mappers;
using Orderflow.Common.Models;
using Orderflow.Features.Trades.Common.Models;
using Orderflow.Features.Trades.Common.Repositories;
using Orderflow.Features.Trades.CreateTrade.Events;
using Orderflow.Features.Trades.CreateTrade.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Services;

public class ProcessTradeServiceTests
{
    private readonly Mock<ITradeRepository> _repositoryMock;
    private readonly Mock<IMapper<Trade, TradeExecutedEvent>> _mapperMock;
    private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
    private readonly ProcessTradeService _processTradeService;

    public ProcessTradeServiceTests()
    {
        _repositoryMock = new Mock<ITradeRepository>();
        _mapperMock = new Mock<IMapper<Trade, TradeExecutedEvent>>();
        _diagnosticContextMock = new Mock<IDiagnosticContext>();

        _processTradeService = new ProcessTradeService(
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
        var result = await _processTradeService.ProcessTrades(trades);

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
        var result = await _processTradeService.ProcessTrades(trades);

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
        var result = await _processTradeService.ProcessTrades(trades);

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(r => r.InsertAsync(trades, It.IsAny<List<TradeExecutedEvent>>()), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<Trade>()), Times.Never);
        _diagnosticContextMock.Verify(d => d.Set("TradeExecuted", false, false), Times.Never);
    }
}