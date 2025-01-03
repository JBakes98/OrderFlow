namespace Orderflow.Api.Routes.Order.Models;

public class PostOrderRequest
{
    public PostOrderRequest(
        int quantity,
        string instrumentId,
        string type)
    {
        Quantity = quantity;
        InstrumentId = instrumentId;
        Type = type;
    }

    public string InstrumentId { get; }
    public int Quantity { get; }
    public string Type { get; }
}