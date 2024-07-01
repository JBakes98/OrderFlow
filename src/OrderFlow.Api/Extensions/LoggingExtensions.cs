using Serilog;
using Serilog.Exceptions;

namespace OrderFlow.Extensions;

public static class LoggingExtensions
{
    public static void RegisterLogging(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("Env", builder.Environment.EnvironmentName)
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddSerilog();

        Log.Logger.Information("Application starting...");

        if (builder.Environment.IsDevelopment())
            Log.Logger.Debug(configuration.Dump());
    }
}