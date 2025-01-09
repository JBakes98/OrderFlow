using Orderflow.Api.Routes.Exchange.Endpoints;

namespace Orderflow.Api.Routes.Exchange;

public static class ExchangeUserGroup
{
    public static void MapExchangeUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("exchanges");

        group.MapGet("/{id}", GetExchange.Handle);
        group.MapGet("/", ListExchange.Handle);

        group.MapPost("/", PostExchange.Handle);
    }
}