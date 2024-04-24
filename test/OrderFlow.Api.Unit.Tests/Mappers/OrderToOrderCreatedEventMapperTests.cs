using OrderFlow.Mappers.Events;
using OrderFlow.Models;

namespace OrderFlow.Api.Unit.Tests.Mappers;

public class OrderToOrderCreatedEventMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_Order_To_OrderCreatedEvent(
        Order source,
        OrderToOrderCreatedEventMapper sut
    )
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id, result.OrderId);
        Assert.Equal(source.Id, result.StreamId);
        Assert.Equal(source.OrderDate, result.CreatedOn);
        Assert.Equal(source.InstrumentId, result.InstrumentId);
        Assert.Equal(source.Quantity, result.Quantity);
        Assert.Equal(source.Price, result.Price);

    }
}