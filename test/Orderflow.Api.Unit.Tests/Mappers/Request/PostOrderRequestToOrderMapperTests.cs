using Orderflow.Api.Routes.Order.Models;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models.Enums;
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

        Assert.Equal(source.InstrumentId, result.InstrumentId);
        Assert.Equal(Enum.Parse<TradeSide>(source.Type), result.TradeSide);
        Assert.Equal(source.Quantity, result.InitialQuantity);

        Assert.NotNull(result.Id);
    }
}