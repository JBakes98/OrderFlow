using Orderflow.Api.Authorization;
using Orderflow.Api.Routes;
using Orderflow.Features.Exchanges.CreateExchange.Endpoints;
using Orderflow.Features.Exchanges.ListExchanges.Endpoints;

namespace Orderflow.Features.Exchanges;

public static class ExchangeUserGroup
{
    public static void MapExchangeUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = Array.Empty<string>();

        var group = app.MapUserGroup("exchanges");

        group.MapGet("/{id}", GetExchange.Endpoints.GetExchange.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Get a exchange");

        group.MapGet("/", ListExchange.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("List exchanges");

        group.MapPost("/", PostExchange.Handle)
            .RequireAuthorization(AuthorizationPolicy.Admin)
            .WithSummary("Create a exchange");
    }
}