using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

namespace OrderFlowHistoryFunction.Extensions;

public static class LoggingExtensions
{
    public static IServiceCollection RegisterLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton(Log.Logger);

        return services;
    }
}