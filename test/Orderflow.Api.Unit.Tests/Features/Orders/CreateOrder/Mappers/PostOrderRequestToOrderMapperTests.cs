using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models.Enums;
using Orderflow.Features.Orders.CreateOrder.Contracts;
using Orderflow.Features.Orders.CreateOrder.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Orders.CreateOrder.Mappers;

public class PostOrderRequestToOrderMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_create_request_to_domain_object(
        PostOrderRequest source,
        PostOrderRequestToOrderMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(Guid.Parse(source.InstrumentId), result.InstrumentId);
        Assert.Equal(Enum.Parse<TradeSide>(source.Side), result.Side);
        Assert.Equal(source.Quantity, result.InitialQuantity);
    }
}