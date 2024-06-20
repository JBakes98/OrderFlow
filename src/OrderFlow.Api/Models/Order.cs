using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Models;

[DynamoDBTable("Order")]
public class Order
{
    public Order()
    {
    }

    public Order(string instrumentId, int quantity, double price)
    {
        InstrumentId = instrumentId;
        Quantity = quantity;
        Price = price;
    }

    [DynamoDBHashKey] public string Id { get; set; } = Guid.NewGuid().ToString();
    public int Quantity { get; set; }
    public string InstrumentId { get; set; }
    public double Price { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
}