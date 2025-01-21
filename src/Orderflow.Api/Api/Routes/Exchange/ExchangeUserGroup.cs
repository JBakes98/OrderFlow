using Orderflow.Api.Authorization;
using Orderflow.Api.Routes.Exchange.Endpoints;

namespace Orderflow.Api.Routes.Exchange;

public static class ExchangeUserGroup
{
    public static void MapExchangeUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = new string[0];

        var group = app.MapUserGroup("exchanges");

        group.MapGet("/{id}", GetExchange.Handle)
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