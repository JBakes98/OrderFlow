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
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 100)
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
        var order = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.sell)
            .With(o => o.Price, 200)
            .Create();

        orderBook.AddOrder(order);

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() =>
            orderBook.AddOrder(order)
        );

        // Assert
        Assert.Equal($"Order with ID {order.Id} already exists.", exception.Message);
    }

    [Fact]
    public void MatchOrders_ShouldMatchBuyAndSellOrdersCorrectly()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 150)
            .Create();

        var sellOrder = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.sell)
            .With(o => o.Price, 150)
            .Create();

        // Act
        orderBook.AddOrder(buyOrder);
        var trades = orderBook.AddOrder(sellOrder);

        // Assert
        Assert.Single(trades);
        var trade = trades[0];
        Assert.Equal(buyOrder.Id, trade.BuyOrderId);
        Assert.Equal(sellOrder.Id, trade.SellOrderId);
        Assert.Equal(150, trade.Price);
    }

    [Fact]
    public void CancelOrders_ShouldRemoveOrderFromBook()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 120)
            .Create();

        orderBook.AddOrder(buyOrder);

        // Act
        orderBook.CancelOrders([buyOrder.Id]);

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
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 150)
            .Create();

        var sellOrder = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.sell)
            .With(o => o.Price, 150)
            .Create();

        orderBook.AddOrder(sellOrder);

        // Act
        var result = orderBook.AddOrder(buyOrder);

        // Assert
        Assert.NotEmpty(result); // Orders match
    }
}