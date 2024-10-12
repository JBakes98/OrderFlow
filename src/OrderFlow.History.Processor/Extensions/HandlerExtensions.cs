using Microsoft.Extensions.DependencyInjection;
using OrderFlowHistoryFunction.Handlers;
using Serilog;

namespace OrderFlowHistoryFunction.Extensions;

public static class HandlerExtensions
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IS3NotificationHandler>(provider => new S3NotificationHandler(
            provider.GetRequiredService<ILogger>()));

        return services;
    }
}