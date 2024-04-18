using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Models;

[DynamoDBTable("Order")]
public class Order
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid InstrumentId { get; set; }
    public double Price { get; set; }
    public DateTime OrderDate { get; set; }
}