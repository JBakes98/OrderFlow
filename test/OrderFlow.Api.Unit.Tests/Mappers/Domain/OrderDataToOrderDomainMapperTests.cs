using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Data.Entities;
using OrderFlow.Mappers.Domain;

namespace OrderFlow.Api.Unit.Tests.Mappers.Domain;

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
        Assert.Equal(source.Quantity, result.Quantity);
        Assert.Equal(source.OrderDate, result.OrderDate);
    }
}