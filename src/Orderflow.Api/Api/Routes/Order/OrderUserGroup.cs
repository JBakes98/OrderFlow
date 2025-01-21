using Orderflow.Api.Authorization;
using Orderflow.Api.Routes.Order.Endpoints;

namespace Orderflow.Api.Routes.Order;

public static class OrderUserGroup
{
    public static void MapOrderUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = Array.Empty<string>();

        var group = app.MapUserGroup("orders");

        group.MapGet("/{id}", GetOrder.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Get a order");

        group.MapGet("/", ListOrder.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("List orders");

        group.MapPost("/", PostOrder.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Create a order");
    }
}