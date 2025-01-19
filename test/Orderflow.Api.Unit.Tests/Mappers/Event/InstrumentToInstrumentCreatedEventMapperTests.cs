using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models;
using Orderflow.Mappers.Events;

namespace Orderflow.Api.Unit.Tests.Mappers.Event;

public class InstrumentToInstrumentCreatedEventMapperTests
{
    [Theory, AutoMoqData]
    public void Should_Map_Instrument_To_InstrumentCreatedEvent(
        Instrument source,
        InstrumentToInstrumentCreatedEventMapper sut
    )
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id.ToString(), result.InstrumentId);
        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.ExchangeId.ToString(), result.Exchange);
        Assert.Equal(source.Sector, result.Sector);
    }
}