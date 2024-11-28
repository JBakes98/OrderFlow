using Orderflow.Api.Routes.Order.Endpoints;

namespace Orderflow.Api.Routes.Order;

public static class OrderUserGroup
{
    public static void MapOrderUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("order");

        group.MapGet("/{id}", GetOrder.Handle);
        group.MapGet("/", ListOrder.Handle);

        group.MapPost("/", PostOrder.Handle);
    }
}