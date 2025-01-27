using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.GetInstrument.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Instruments.GetInstrument.Mappers;

public class InstrumentToGetInstrumentResponseMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_object_to_get_response(
        Instrument source,
        InstrumentToGetInstrumentResponseMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id.ToString(), result.Id);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Sector, result.Sector);
        Assert.Equal(source.ExchangeId.ToString(), result.Exchange);
    }
}