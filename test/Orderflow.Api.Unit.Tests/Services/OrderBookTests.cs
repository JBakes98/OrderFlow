using AutoFixture;
using Orderflow.Domain.Models;
using Orderflow.Domain.Models.Enums;
using Orderflow.Services;

namespace Orderflow.Api.Unit.Tests.Services;

public class OrderBookTests
{
    private readonly IFixture _fixture = new Fixture();

    [Fact]
    public void AddOrder_ShouldAddOrderToCorrectBook()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder = _fixture.Build<Order>()
            .With(o => o.TradeSide, TradeSide.buy)
            .With(o => o.Price, 100)
            .With(o => o.Id, "BuyOrder1")
            .Create();

        // Act
        var trades = orderBook.AddOrder(buyOrder);

        // Assert
        Assert.Empty(trades); // No trades should occur for a single buy order
    }

    [Fact]
    public void AddOrder_ShouldThrowException_WhenOrderAlreadyExists()
    {
        // Arrange
        var orderBook = new OrderBook();
        var duplicateOrder = _fixture.Build<Order>()
            .With(o => o.Id, "Order1")
            .With(o => o.TradeSide, TradeSide.sell)
            .With(o => o.Price, 200)
            .Create();

        orderBook.AddOrder(duplicateOrder);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            orderBook.AddOrder(duplicateOrder));
        Assert.Equal("Order with ID Order1 already exists.", exception.Message);
    }

    [Fact]
    public void MatchOrders_ShouldMatchBuyAndSellOrdersCorrectly()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder = _fixture.Build<Order>()
            .With(o => o.TradeSide, TradeSide.buy)
            .With(o => o.Price, 150)
            .With(o => o.Id, "BuyOrder1")
            .Create();

        var sellOrder = _fixture.Build<Order>()
            .With(o => o.TradeSide, TradeSide.sell)
            .With(o => o.Price, 150)
            .With(o => o.Id, "SellOrder1")
            .Create();

        // Act
        orderBook.AddOrder(buyOrder);
        var trades = orderBook.AddOrder(sellOrder);

        // Assert
        Assert.Single(trades);
        var trade = trades[0];
        Assert.Equal("BuyOrder1", trade.BidTrade.OrderId);
        Assert.Equal("SellOrder1", trade.AskTrade.OrderId);
        Assert.Equal(150, trade.BidTrade.Price);
        Assert.Equal(150, trade.AskTrade.Price);
    }

    [Fact]
    public void CancelOrders_ShouldRemoveOrderFromBook()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder = _fixture.Build<Order>()
            .With(o => o.TradeSide, TradeSide.buy)
            .With(o => o.Price, 120)
            .With(o => o.Id, "BuyOrder1")
            .Create();

        orderBook.AddOrder(buyOrder);

        // Act
        orderBook.CancelOrders(["BuyOrder1"]);

        // Assert
        var trades = orderBook.AddOrder(buyOrder);
        Assert.Empty(trades);
    }

    [Fact]
    public void CanMatch_ShouldReturnTrue_WhenBuyPriceMeetsBestAsk()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder = _fixture.Build<Order>()
            .With(o => o.TradeSide, TradeSide.buy)
            .With(o => o.Price, 150)
            .Create();

        var sellOrder = _fixture.Build<Order>()
            .With(o => o.TradeSide, TradeSide.sell)
            .With(o => o.Price, 150)
            .Create();

        orderBook.AddOrder(sellOrder);

        // Act
        var result = orderBook.AddOrder(buyOrder);

        // Assert
        Assert.NotEmpty(result); // Orders match
    }
}