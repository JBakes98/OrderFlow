namespace OrderFlow.Contracts.Requests;

public class CreateOrder
{
    public CreateOrder(string id, int quantity, double price, string instrumentId)
    {
        Id = id;
        Quantity = quantity;
        Price = price;
        InstrumentId = instrumentId;
    }

    public string InstrumentId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Id { get; }
}