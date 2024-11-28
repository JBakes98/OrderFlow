using Orderflow.Api.Routes.Order.Models;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Mappers.Request;

namespace Orderflow.Api.Unit.Tests.Mappers.Request;

public class PostOrderRequestToOrderMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_create_request_to_domain_object(
        PostOrderRequest source,
        PostOrderRequestToOrderMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.InstrumentId.ToString(), result.InstrumentId);
        Assert.Equal(source.Type, result.Type);
        Assert.Equal(source.Quantity, result.Quantity);

        Assert.NotNull(result.Id);
    }
}