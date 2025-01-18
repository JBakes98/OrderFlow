using Orderflow.Api.Routes.Exchange.Endpoints;

namespace Orderflow.Api.Routes.Exchange;

public static class ExchangeUserGroup
{
    public static void MapExchangeUserGroup(this WebApplication app)
    {
        var group = app.MapUserGroup("exchanges");

        group.MapGet("/{id}", GetExchange.Handle)
            .WithSummary("Get a exchange");
        group.MapGet("/", ListExchange.Handle)
            .WithSummary("List exchanges");

        group.MapPost("/", PostExchange.Handle)
            .WithSummary("Create a exchange");
    }
}