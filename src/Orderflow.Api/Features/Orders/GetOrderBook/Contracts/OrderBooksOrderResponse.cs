namespace Orderflow.Features.Orders.GetOrderBook.Contracts;

public record OrderBooksOrderResponse
{
    public int Quantity { get; init; }
    public double Price { get; init; }
    public DateTime Placed { get; init; }
    public double Value { get; init; }
}