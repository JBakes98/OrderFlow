using AutoFixture;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Mappers.Events;

namespace Orderflow.Api.Unit.Tests.Mappers.Event;

public class OrderToOrderRaisedEventMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_BuyOrder_To_OrderCreatedEvent(
        Order source,
        OrderToOrderRaisedEventMapper sut
    )
    {
        var result = sut.Map(source);

        Assert.IsType<BuyOrderRaised>(result);

        Assert.Equal(source.Id, result.OrderId);
        Assert.Equal(source.InstrumentId, result.InstrumentId);
        Assert.Equal(source.Quantity, result.Quantity);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.Value, result.Value);
        Assert.Equal(source.Status.ToString(), result.Status);
    }
}