namespace Orderflow.Api.Routes.Order;

public static class OrderUserGroup
{
    public static void MapOrderUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("order");
    }
}