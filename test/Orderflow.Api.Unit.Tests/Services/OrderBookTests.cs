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

    [Fact]
    public void CancelOrders_ShouldRemoveMultipleOrdersFromBook()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder1 = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 100)
            .Create();

        var buyOrder2 = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 105)
            .Create();

        var sellOrder = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.sell)
            .With(o => o.Price, 110)
            .Create();

        orderBook.AddOrder(buyOrder1);
        orderBook.AddOrder(buyOrder2);
        orderBook.AddOrder(sellOrder);

        // Act
        orderBook.CancelOrders(new List<Guid> { buyOrder1.Id, sellOrder.Id });

        // Assert
        var (bids, asks) = orderBook.GetOrderBook();
        Assert.DoesNotContain(buyOrder1, bids);
        Assert.DoesNotContain(sellOrder, asks);
        Assert.Contains(buyOrder2, bids); // Ensure other orders are not affected
    }

    [Fact]
    public void GetOrderBook_WhenBidsAndAsksAreEmpty_ReturnsEmptyLists()
    {
        // Arrange
        var orderBook = new OrderBook();

        // Act
        var (bids, asks) = orderBook.GetOrderBook();

        // Assert
        Assert.NotNull(bids);
        Assert.NotNull(asks);
        Assert.Empty(bids);
        Assert.Empty(asks);
    }

    [Fact]
    public void GetOrderBook_ShouldReturnCurrentBidsAndAsks()
    {
        // Arrange
        var orderBook = new OrderBook();
        var buyOrder1 = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 100)
            .Create();

        var buyOrder2 = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.buy)
            .With(o => o.Price, 105)
            .Create();

        var sellOrder1 = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.sell)
            .With(o => o.Price, 110)
            .Create();

        var sellOrder2 = _fixture.Build<Order>()
            .With(o => o.Side, TradeSide.sell)
            .With(o => o.Price, 115)
            .Create();

        orderBook.AddOrder(buyOrder1);
        orderBook.AddOrder(buyOrder2);
        orderBook.AddOrder(sellOrder1);
        orderBook.AddOrder(sellOrder2);

        // Act
        var (bids, asks) = orderBook.GetOrderBook();

        // Assert
        // Verify bids
        Assert.Equal(2, bids.Count); // Ensure two buy orders are present
        Assert.Contains(buyOrder1, bids);
        Assert.Contains(buyOrder2, bids);

        // Verify asks
        Assert.Equal(2, asks.Count); // Ensure two sell orders are present
        Assert.Contains(sellOrder1, asks);
        Assert.Contains(sellOrder2, asks);

        // Ensure orders are sorted
        Assert.Equal(buyOrder2.Price, bids[0].Price); // Highest buy price first
        Assert.Equal(buyOrder1.Price, bids[1].Price); // Second highest buy price
        Assert.Equal(sellOrder1.Price, asks[0].Price); // Lowest sell price first
        Assert.Equal(sellOrder2.Price, asks[1].Price); // Second lowest sell price
    }
}