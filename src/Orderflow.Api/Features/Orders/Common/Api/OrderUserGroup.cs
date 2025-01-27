using Orderflow.Api.Authorization;
using Orderflow.Api.Routes;
using Orderflow.Features.Orders.CreateOrder.Endpoints;

namespace Orderflow.Features.Orders;

public static class OrderUserGroup
{
    public static void MapOrderUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = Array.Empty<string>();

        var group = app.MapUserGroup("orders");

        group.MapGet("/{id}", GetOrder.Endpoints.GetOrder.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Get a order");

        group.MapGet("/", ListOrders.Endpoints.ListOrders.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("List orders");

        group.MapPost("/", PostOrder.Handle)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Create a order");
    }
}