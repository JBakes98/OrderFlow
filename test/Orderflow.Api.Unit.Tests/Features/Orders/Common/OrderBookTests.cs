using Orderflow.Features.Common.Enums;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Services;

namespace Orderflow.Api.Unit.Tests.Features.Orders.Common;

public class OrderBookTests
{
    private readonly OrderBook _orderBook;

    public OrderBookTests()
    {
        _orderBook = new OrderBook();
    }

    [Fact]
    public void AddOrder_ShouldAddOrderToCorrectBook_WhenOrderIsBuy()
    {
        // Arrange
        var order = new Order(10, Guid.NewGuid(), 100.0, TradeSide.buy);

        // Act
        var trades = _orderBook.AddOrder(order);

        // Assert
        var (bids, asks) = _orderBook.GetOrderBook();
        Assert.Empty(trades);
        Assert.Single(bids);
        Assert.Empty(asks);
        Assert.Contains(order, bids);
    }

    [Fact]
    public void AddOrder_ShouldAddOrderToCorrectBook_WhenOrderIsSell()
    {
        // Arrange
        var order = new Order(10, Guid.NewGuid(), 100.0, TradeSide.sell);

        // Act
        var trades = _orderBook.AddOrder(order);

        // Assert
        var (bids, asks) = _orderBook.GetOrderBook();
        Assert.Empty(trades);
        Assert.Empty(bids);
        Assert.Single(asks);
        Assert.Contains(order, asks);
    }

    [Fact]
    public void AddOrder_ShouldMatchOrders_WhenBuyAndSellOrdersCross()
    {
        // Arrange
        var buyOrder = new Order(10, Guid.NewGuid(), 100.0, TradeSide.buy);
        var sellOrder = new Order(10, Guid.NewGuid(), 100.0, TradeSide.sell);
        _orderBook.AddOrder(buyOrder);

        // Act
        var trades = _orderBook.AddOrder(sellOrder);

        // Assert
        var trade = trades.First();
        Assert.Single(trades);
        Assert.Equal(buyOrder.Id, trade.BuyOrderId);
        Assert.Equal(sellOrder.Id, trade.SellOrderId);
        Assert.Equal(100.0, trade.Price);
        Assert.Equal(10, trade.Quantity);

        var (bids, asks) = _orderBook.GetOrderBook();
        Assert.Empty(bids);
        Assert.Empty(asks);
    }

    [Fact]
    public void AddOrder_ShouldPartiallyFillOrders_WhenQuantitiesDiffer()
    {
        // Arrange
        var buyOrder = new Order(20, Guid.NewGuid(), 100.0, TradeSide.buy);
        var sellOrder = new Order(10, Guid.NewGuid(), 100.0, TradeSide.sell);
        _orderBook.AddOrder(buyOrder);

        // Act
        var trades = _orderBook.AddOrder(sellOrder);

        // Assert
        var trade = trades.First();
        Assert.Single(trades);
        Assert.Equal(10, trade.Quantity);

        var (bids, asks) = _orderBook.GetOrderBook();
        Assert.Single(bids);
        Assert.Empty(asks);
        Assert.Equal(10, bids.First().GetRemainingQuantity());
    }

    [Fact]
    public void CancelOrders_ShouldRemoveOrdersFromBook()
    {
        // Arrange
        var buyOrder = new Order(10, Guid.NewGuid(), 100.0, TradeSide.buy);
        var sellOrder = new Order(10, Guid.NewGuid(), 101.0, TradeSide.sell);
        _orderBook.AddOrder(buyOrder);
        _orderBook.AddOrder(sellOrder);

        // Act
        _orderBook.CancelOrders(new List<Guid> { buyOrder.Id });

        // Assert
        var (bids, asks) = _orderBook.GetOrderBook();
        Assert.Empty(bids);
        Assert.Single(asks);
        Assert.Contains(sellOrder, asks);
    }

    [Fact]
    public void CancelOrders_ShouldNotThrow_WhenOrderDoesNotExist()
    {
        // Act & Assert
        var exception = Record.Exception(() => _orderBook.CancelOrders(new List<Guid> { Guid.NewGuid() }));
        Assert.Null(exception);
    }

    [Fact]
    public void GetOrderBook_ShouldReturnAllBidsAndAsks()
    {
        // Arrange
        var buyOrder = new Order(10, Guid.NewGuid(), 100.0, TradeSide.buy);
        var sellOrder = new Order(10, Guid.NewGuid(), 101.0, TradeSide.sell);
        _orderBook.AddOrder(buyOrder);
        _orderBook.AddOrder(sellOrder);

        // Act
        var (bids, asks) = _orderBook.GetOrderBook();

        // Assert
        Assert.Single(bids);
        Assert.Single(asks);
        Assert.Contains(buyOrder, bids);
        Assert.Contains(sellOrder, asks);
    }

    [Fact]
    public void AddOrder_ShouldThrow_WhenOrderWithSameIdExists()
    {
        // Arrange
        var order = new Order(10, Guid.NewGuid(), 100.0, TradeSide.buy);
        _orderBook.AddOrder(order);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _orderBook.AddOrder(order));
        Assert.Equal($"Order with ID {order.Id} already exists.", exception.Message);
    }
}