using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Common.Webhooks;

namespace Orderflow.Features.Orders.Common.Endpoints;

public class PostWebhookSubscribe
{
    public static async Task<Results<Ok, ProblemHttpResult>> Handle(
        HttpContext context,
        IWebhookService webhookService,
        [FromBody] string callbackUrl)
    {
        if (string.IsNullOrWhiteSpace(callbackUrl))
        {
            return TypedResults.Problem("Invalid callback URL");
        }

        webhookService.Subscribe(callbackUrl);

        return TypedResults.Ok();
    }
}