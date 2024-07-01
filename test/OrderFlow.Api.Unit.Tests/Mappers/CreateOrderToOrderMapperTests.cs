using OrderFlow.Contracts.Requests;
using OrderFlow.Mappers.Request;

namespace OrderFlow.Api.Unit.Tests.Mappers;

public class CreateOrderToOrderMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_CreateOrder_To_Order(
        CreateOrder source,
        CreateOrderToOrderMapper sut
    )
    {
        var result = sut.Map(source);

        Assert.Equal(source.InstrumentId.ToString(), result.InstrumentId);
        Assert.Equal(source.Quantity, result.Quantity);

        Assert.NotNull(result.Id);
    }
}