using System.Text.Json.Serialization;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using Orderflow.Common.Api.Authorization;
using Orderflow.Common.Extensions;
using Orderflow.Features.AlphaVantage;
using Orderflow.Features.Exchanges.Common.Api;
using Orderflow.Features.Instruments.Common.Api;
using Orderflow.Features.Orders.Common.Api;
using Scalar.AspNetCore;
using Serilog;

namespace Orderflow;

public static class Program
{
    static void Main(string[] args)
    {
        using MeterProvider meterProvider = Sdk.CreateMeterProviderBuilder()
            .AddMeter("Orderflow.Orderbook")
            .AddPrometheusHttpListener(
                opt => opt.UriPrefixes = ["http://localhost:9464/"])
            .Build();

        var builder = WebApplication.CreateBuilder(args);

        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false)
            .AddEnvironmentVariables("ORDERFLOW_");

        var config = configBuilder.Build();

        builder.RegisterLogging(config);
        builder.AddOtel(config);

        builder.Services.RegisterAwsServices(config);
        builder.Services.RegisterPostgres(config);
        builder.Services.RegisterServices(config);
        builder.Services.RegisterValidators();
        builder.Services.RegisterAlphaVantage(config);

        builder.Services.RegisterAuthentication(config);
        builder.Services.AddAuthorization(opt => { opt.AddAuthorizationPolicies(); });
        builder.Services.RegisterCors(config);

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        builder.Services.AddProblemDetails();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapPrometheusScrapingEndpoint();
        app.UseSerilogRequestLogging();

        app.UseCors("OrderflowDashboard");
        app.UseHttpsRedirection();
        app.UseExceptionHandler();

        app.MapExchangeUserGroup();
        app.MapInstrumentUserGroup();
        app.MapOrderUserGroup();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.Run();
    }
}