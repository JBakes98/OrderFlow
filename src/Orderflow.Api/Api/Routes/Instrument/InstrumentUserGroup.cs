using Orderflow.Api.Routes.Instrument.Endpoints;

namespace Orderflow.Api.Routes.Instrument;

public static class InstrumentUserGroup
{
    public static void MapInstrumentUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("instruments");

        group.MapGet("/{id}", GetInstrument.Handle)
            .WithSummary("Get a instrument");

        group.MapGet("/", ListInstrument.Handle)
            .WithSummary("List instruments");


        group.MapPost("/", PostInstrument.Handle)
            .WithSummary("Create a instrument");
    }
}