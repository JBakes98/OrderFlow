using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Domain.Models;
using OrderFlow.Mappers.Events;

namespace OrderFlow.Api.Unit.Tests.Mappers.Event;

public class InstrumentToInstrumentCreatedEventMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_Instrument_To_InstrumentCreatedEvent(
        Instrument source,
        InstrumentToInstrumentCreatedEventMapper sut
    )
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id, result.InstrumentId);
        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Exchange, result.Exchange);
        Assert.Equal(source.Sector, result.Sector);
    }
}