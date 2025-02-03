namespace Orderflow.Common.Webhooks;

public class WebhookSubscription
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string CallbackUrl { get; }

    public WebhookSubscription(string callbackUrl) => CallbackUrl = callbackUrl;
}