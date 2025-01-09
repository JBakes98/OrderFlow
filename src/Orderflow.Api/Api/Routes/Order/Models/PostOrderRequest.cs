namespace Orderflow.Api.Routes.Order.Models;

public class PostOrderRequest
{
    public PostOrderRequest(
        int quantity,
        string instrumentId,
        string side,
        double price)
    {
        Quantity = quantity;
        InstrumentId = instrumentId;
        Side = side;
        Price = price;
    }

    public string InstrumentId { get; }
    public int Quantity { get; }
    public string Side { get; }
    public double Price { get; }
}