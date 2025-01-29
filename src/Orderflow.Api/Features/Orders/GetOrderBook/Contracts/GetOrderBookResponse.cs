namespace Orderflow.Features.Orders.GetOrderBook.Contracts;

public record GetOrderBookResponse
{
    public required IEnumerable<OrderBooksOrderResponse> Bids { get; init; }
    public required IEnumerable<OrderBooksOrderResponse> Asks { get; init; }
}