using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Domain.Models;
using OrderFlow.Mappers.Data;

namespace OrderFlow.Api.Unit.Tests.Mappers.Data;

public class InstrumentDomainToInstrumentDataMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_to_entity_object(
        Instrument source,
        InstrumentDomainToInstrumentDataMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Exchange, result.Exchange);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Sector, result.Sector);
        Assert.Equal(source.Id, result.Id);
    }
}