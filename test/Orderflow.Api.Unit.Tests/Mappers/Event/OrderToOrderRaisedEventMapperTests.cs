using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models;
using Orderflow.Mappers.Events;

namespace Orderflow.Api.Unit.Tests.Mappers.Event;

public class OrderToOrderRaisedEventMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_Order_To_OrderCreatedEvent(
        Order source,
        OrderToOrderRaisedEventMapper sut
    )
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id, result.OrderId);
        Assert.Equal(source.InstrumentId, result.InstrumentId);
        Assert.Equal(source.Quantity, result.Quantity);
        Assert.Equal(source.Price, result.Price);
    }
}