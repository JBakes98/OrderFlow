using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models;
using Orderflow.Mappers.Api.Response;

namespace Orderflow.Api.Unit.Tests.Mappers.Api.Response;

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