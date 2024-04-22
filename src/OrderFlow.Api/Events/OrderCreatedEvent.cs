using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Events;

[DynamoDBTable("OrderEventsTable")]
public class OrderCreatedEvent : BaseOrderEvent
{
    public OrderCreatedEvent(
        string orderId, 
        string instrumentId, 
        int quantity, 
        double price, 
        DateTime createdOn) 
        : base(
            orderId, 
            instrumentId, 
            quantity, 
            price, 
            createdOn)
    {
    }
}