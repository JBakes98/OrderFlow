using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderFlowHistoryFunction.Extensions;
using OrderFlowHistoryFunction.Handlers;
using Serilog;
using ConfigurationExtensions = OrderFlowHistoryFunction.Extensions.ConfigurationExtensions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace OrderFlowHistoryFunction;

public class Function
{
    private readonly ServiceCollection _serviceCollection = new();
    private readonly IS3NotificationHandler _s3NotificationHandler;
    private readonly ILogger _logger;

    public Function() : this(ConfigurationExtensions.Build("OrderFlowHistory"))
    {
    }

    private Function(IConfiguration configuration)
    {
        _serviceCollection
            .RegisterLogging(configuration)
            .RegisterHandlers();

        var serviceProvider = _serviceCollection.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });

        _s3NotificationHandler = serviceProvider.GetRequiredService<IS3NotificationHandler>();
        _logger = Log.Logger;
    }


    public async Task<string> FunctionHandler(S3Event.S3EventNotificationRecord notification, ILambdaContext context)
    {
        _logger.Information("Starting function");
        var result = await _s3NotificationHandler.HandleAsync(notification);
        _logger.Information("Function finished");

        return result.ToString();
    }
}