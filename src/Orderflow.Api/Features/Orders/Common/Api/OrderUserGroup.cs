using Orderflow.Common.Api.Routes;
using Orderflow.Common.Webhooks;
using Orderflow.Features.Orders.CreateOrder.Endpoints;

namespace Orderflow.Features.Orders.Common.Api;

public static class OrderUserGroup
{
    public static void MapOrderUserGroup(this WebApplication app)
    {
        var extraRequiredPolicies = Array.Empty<string>();

        var group = app.MapUserGroup("orders");

        group.MapGet("/{id}", GetOrder.Endpoints.GetOrder.Handle)
            // .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Get a order");

        group.MapGet("/", ListOrders.Endpoints.ListOrders.Handle)
            // .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("List orders");

        group.MapPost("/", PostOrder.Handle)
            // .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies])
            .WithSummary("Create a order");

        group.MapGet("/orderbook/{id}", GetOrderBook.Endpoints.GetOrderBook.Handle)
            .WithSummary("Get an instruments Orderbook");

        // Subscribe to webhook
        app.MapPost("/webhooks/subscribe", Endpoints.PostWebhookSubscribe.Handle)
            .WithSummary("Subscribe to webhook");

        // Notify all subscribers (trigger webhook updates)
        app.MapPost("/webhooks/notify", async (object payload) =>
        {
            var webhookService = new WebhookService();

            await webhookService.NotifySubscribersAsync(payload);
            return Results.Ok();
        });
    }
}