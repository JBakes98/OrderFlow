using AutoFixture;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models.Enums;
using Orderflow.Features.Orders.Common;
using Orderflow.Features.Orders.CreateOrder.Events;
using Orderflow.Features.Orders.CreateOrder.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Orders.CreateOrder.Mappers;

public class OrderToOrderRaisedEventMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_BuyOrder_To_BuyOrderRaisedEvent(
        OrderToOrderRaisedEventMapper sut,
        IFixture fixture
    )
    {
        var source = fixture.Customize(new OrderCustomization()).Create<Order>();
        var result = sut.Map(source);

        Assert.IsType<BuyOrderRaised>(result);

        Assert.Equal(source.Id.ToString(), result.OrderId);
        Assert.Equal(source.InstrumentId.ToString(), result.InstrumentId);
        Assert.Equal(source.InitialQuantity, result.Quantity);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.Value, result.Value);
        Assert.Equal(source.Status.ToString(), result.Status);
    }

    [Theory, AutoMoqData]
    public void Should_Map_SellOrder_To_SellOrderRaisedEvent(
        OrderToOrderRaisedEventMapper sut,
        IFixture fixture
    )
    {
        var source = fixture.Customize(new OrderCustomization(TradeSide.sell)).Create<Order>();
        var result = sut.Map(source);

        Assert.IsType<SellOrderRaised>(result);

        Assert.Equal(source.Id.ToString(), result.OrderId);
        Assert.Equal(source.InstrumentId.ToString(), result.InstrumentId);
        Assert.Equal(source.InitialQuantity, result.Quantity);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.Value, result.Value);
        Assert.Equal(source.Status.ToString(), result.Status);
    }
}