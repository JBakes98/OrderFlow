using Orderflow.Api.Authorization;
using Orderflow.Api.Routes.Instrument.Endpoints;

namespace Orderflow.Api.Routes.Instrument;

public static class InstrumentUserGroup
{
    public static void MapInstrumentUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = Array.Empty<string>();

        var group = app.MapUserGroup("instruments");

        group.MapGet("/{id}", GetInstrument.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Get a instrument");

        group.MapGet("/", ListInstrument.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("List instruments");


        group.MapPost("/", PostInstrument.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Create a instrument");
    }
}