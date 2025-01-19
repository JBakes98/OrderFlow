using Orderflow.Api.Routes.Order.Endpoints;

namespace Orderflow.Api.Routes.Order;

public static class OrderUserGroup
{
    public static void MapOrderUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("orders");

        group.MapGet("/{id}", GetOrder.Handle)
            .WithSummary("Get a order");

        group.MapGet("/", ListOrder.Handle)
            .WithSummary("List orders");

        group.MapPost("/", PostOrder.Handle)
            .WithSummary("Create a order");
    }
}