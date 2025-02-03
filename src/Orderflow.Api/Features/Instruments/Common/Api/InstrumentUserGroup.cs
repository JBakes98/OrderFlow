using Orderflow.Common.Api.Routes;
using Orderflow.Features.Instruments.CreateInstrument.Endpoints;
using Orderflow.Features.Instruments.ListInstruments.Endpoints;

namespace Orderflow.Features.Instruments.Common.Api;

public static class InstrumentUserGroup
{
    public static void MapInstrumentUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = Array.Empty<string>();

        var group = app.MapUserGroup("instruments");

        group.MapGet("/{id}", GetInstrument.Endpoints.GetInstrument.Handle)
            // .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Get a instrument");

        group.MapGet("/", ListInstrument.Handle)
            // .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("List instruments");


        group.MapPost("/", PostInstrument.Handle)
            // .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Create a instrument");
    }
}