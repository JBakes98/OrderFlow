using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Data.Entities;
using Orderflow.Mappers.Domain;

namespace Orderflow.Api.Unit.Tests.Mappers.Domain;

public class OrderDataToOrderDomainMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_to_entity_object(
        OrderEntity source,
        OrderDataToOrderDomainMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id, result.Id);
        Assert.Equal(source.InstrumentId, result.InstrumentId);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.InitialQuantity, result.InitialQuantity);
        Assert.Equal(source.RemainingQuantity, result.RemainingQuantity);
        Assert.Equal(source.Date, result.Date);
        Assert.Equal(source.Type, result.TradeSide);
        Assert.Equal(source.Status, result.Status);
    }
}