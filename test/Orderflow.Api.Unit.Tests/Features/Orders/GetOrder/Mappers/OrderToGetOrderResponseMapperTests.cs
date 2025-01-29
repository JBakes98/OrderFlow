using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.GetOrder.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Orders.GetOrder.Mappers;

public class OrderToGetOrderResponseMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_object_to_get_response(
        Order source,
        OrderToGetOrderResponseMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id.ToString(), result.Id);
        Assert.Equal(source.Placed, result.Placed);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.Side, result.Side);
        Assert.Equal(source.Status, result.Status);
        Assert.Equal(source.InitialQuantity, result.Quantity);
        Assert.Equal(source.RemainingQuantity, result.RemainingQuantity);
        Assert.Equal(source.Value, result.Value);
        Assert.Equal(source.Placed, result.Placed);
        Assert.Equal(source.InstrumentId.ToString(), result.InstrumentId);
    }
}