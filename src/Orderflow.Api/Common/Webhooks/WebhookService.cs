namespace Orderflow.Common.Webhooks;

public class WebhookService : IWebhookService
{
    private readonly List<WebhookSubscription> _subscribers = [];
    private readonly HttpClient _httpClient = new();

    public void Subscribe(string callbackUrl)
    {
        _subscribers.Add(new WebhookSubscription(callbackUrl));
    }

    public async Task NotifySubscribersAsync(object payload)
    {
        foreach (var subscriber in _subscribers)
        {
            try
            {
                await _httpClient.PostAsJsonAsync(subscriber.CallbackUrl, payload);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to notify {subscriber.CallbackUrl}: {ex.Message}");
            }
        }
    }
}