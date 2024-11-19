using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Domain.Models;
using OrderFlow.Mappers.Data;

namespace OrderFlow.Api.Unit.Tests.Mappers.Data;

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
        Assert.Equal(source.Quantity, result.Quantity);
        Assert.Equal(source.Date, result.Date);
    }
}