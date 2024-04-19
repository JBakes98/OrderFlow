namespace OrderFlow.Contracts.Requests;

public class CreateOrder
{
    public CreateOrder(int quantity, double price, string instrumentId)
    {
        Quantity = quantity;
        Price = price;
        InstrumentId = instrumentId;
    }

    public string InstrumentId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}