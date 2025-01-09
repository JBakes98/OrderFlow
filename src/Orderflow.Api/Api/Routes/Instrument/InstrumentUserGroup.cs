using Orderflow.Api.Routes.Instrument.Endpoints;

namespace Orderflow.Api.Routes.Instrument;

public static class InstrumentUserGroup
{
    public static void MapInstrumentUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("instruments");

        group.MapGet("/{id}", GetInstrument.Handle);
        group.MapGet("/", ListInstrument.Handle);

        group.MapPost("/", PostInstrument.Handle);
    }
}