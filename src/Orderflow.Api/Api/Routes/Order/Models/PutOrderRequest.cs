namespace Orderflow.Api.Routes.Order.Models;

public class PutOrderRequest
{
    public PutOrderRequest(
        string id,
        string status)
    {
        Id = id;
        Status = status;
    }

    public string Id { get; private set; }
    public string Status { get; }

    public void SetOrderId(string id)
    {
        Id = id;
    }
}