using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Orders.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;

namespace Orderflow.Api.Unit.Tests.Features.Orders.Common.Mappers;

public class OrderDomainToOrderDataMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_to_entity_object(
        Order source,
        OrderDomainToOrderDataMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id, result.Id);
        Assert.Equal(source.InstrumentId, result.InstrumentId);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.InitialQuantity, result.InitialQuantity);
        Assert.Equal(source.RemainingQuantity, result.RemainingQuantity);
        Assert.Equal(source.Placed, result.Placed);
        Assert.Equal(source.Updated, result.Updated);
        Assert.Equal(source.Side, result.Side);
        Assert.Equal(source.Status, result.Status);
    }
}