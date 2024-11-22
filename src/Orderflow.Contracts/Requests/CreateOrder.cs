namespace Orderflow.Contracts.Requests;

public class CreateOrder
{
    public CreateOrder(int quantity, Guid instrumentId)
    {
        Quantity = quantity;
        InstrumentId = instrumentId;
    }

    public Guid InstrumentId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}