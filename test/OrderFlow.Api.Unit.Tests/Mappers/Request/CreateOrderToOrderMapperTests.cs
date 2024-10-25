using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Contracts.Requests;
using OrderFlow.Mappers.Request;

namespace OrderFlow.Api.Unit.Tests.Mappers.Request;

public class CreateOrderToOrderMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_create_request_to_domain_object(
        CreateOrder source,
        CreateOrderToOrderMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.InstrumentId.ToString(), result.InstrumentId);
        Assert.Equal(source.Price, result.Price);
        Assert.Equal(source.Quantity, result.Quantity);

        Assert.NotNull(result.Id);
    }
}