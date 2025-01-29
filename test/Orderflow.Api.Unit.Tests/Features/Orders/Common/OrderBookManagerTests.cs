using Orderflow.Features.Orders.Common.Services;

namespace Orderflow.Api.Unit.Tests.Features.Orders.Common;

public class OrderBookManagerTests
{
    [Fact]
    public void AddOrderBook_ShouldAddNewOrderBook()
    {
        // Arrange
        var manager = new OrderBookManager();
        var instrumentId = Guid.NewGuid();

        // Act
        manager.AddOrderBook(instrumentId);

        // Assert
        var orderBook = manager.GetOrderBook(instrumentId);
        Assert.NotNull(orderBook);
    }

    [Fact]
    public void AddOrderBook_ShouldNotAddDuplicateOrderBook()
    {
        // Arrange
        var manager = new OrderBookManager();
        var instrumentId = Guid.NewGuid();

        manager.AddOrderBook(instrumentId);
        var initialOrderBook = manager.GetOrderBook(instrumentId);

        // Act
        manager.AddOrderBook(instrumentId);
        var duplicateOrderBook = manager.GetOrderBook(instrumentId);

        // Assert
        Assert.Same(initialOrderBook, duplicateOrderBook);
    }

    [Fact]
    public void GetOrderBook_ShouldReturnExistingOrderBook()
    {
        // Arrange
        var manager = new OrderBookManager();
        var instrumentId = Guid.NewGuid();

        manager.AddOrderBook(instrumentId);

        // Act
        var orderBook = manager.GetOrderBook(instrumentId);

        // Assert
        Assert.NotNull(orderBook);
    }

    [Fact]
    public void GetOrderBook_ShouldCreateOrderBookIfNotExists()
    {
        // Arrange
        var manager = new OrderBookManager();
        var instrumentId = Guid.NewGuid();

        // Act
        var orderBook = manager.GetOrderBook(instrumentId);

        // Assert
        Assert.NotNull(orderBook);
    }

    [Fact]
    public void RemoveOrderBook_ShouldRemoveOrderBookIfExists()
    {
        // Arrange
        var manager = new OrderBookManager();
        var instrumentId = Guid.NewGuid();


        manager.AddOrderBook(instrumentId);

        // Act
        manager.RemoveOrderBook(instrumentId);
        var orderBook = manager.GetOrderBook(instrumentId);

        // Assert
        Assert.NotNull(orderBook);
    }

    [Fact]
    public void RemoveOrderBook_ShouldNotThrowIfOrderBookDoesNotExist()
    {
        // Arrange
        var manager = new OrderBookManager();
        var instrumentId = Guid.NewGuid();

        // Act & Assert
        var exception = Record.Exception(() => manager.RemoveOrderBook(instrumentId));
        Assert.Null(exception);
    }
}