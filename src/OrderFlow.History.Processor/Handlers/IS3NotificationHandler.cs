using Amazon.Lambda.S3Events;

namespace OrderFlowHistoryFunction.Handlers;

public interface IS3NotificationHandler
{
    Task<bool> HandleAsync(S3Event.S3EventNotificationRecord notification);
}