using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.Common.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Instruments.Common.Mappers;

public class InstrumentDomainToInstrumentDataMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_to_entity_object(
        Instrument source,
        InstrumentDomainToInstrumentDataMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.ExchangeId, result.ExchangeId);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Sector, result.Sector);
        Assert.Equal(source.Id, result.Id);
    }
}