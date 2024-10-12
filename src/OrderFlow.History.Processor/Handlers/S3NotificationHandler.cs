using Amazon.Lambda.S3Events;
using Ardalis.GuardClauses;
using Serilog;

namespace OrderFlowHistoryFunction.Handlers;

public class S3NotificationHandler : IS3NotificationHandler
{
    private readonly ILogger _logger;

    public S3NotificationHandler(ILogger logger)
    {
        _logger = Guard.Against.Null(logger);
    }

    public async Task<bool> HandleAsync(S3Event.S3EventNotificationRecord notification)
    {
        return true;
    }
}