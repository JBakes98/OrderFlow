namespace Orderflow.Common.Webhooks;

public interface IWebhookService
{
    public void Subscribe(string callbackUrl);
    public Task NotifySubscribersAsync(object payload);
}