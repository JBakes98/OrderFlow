using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Orderflow.Common.Extensions;

public static class OtelExtensions
{
    public static void AddOtel(this WebApplicationBuilder builder, IConfiguration config)
    {
        var tracingOtlpEndpoint = builder.Configuration["OTLP_ENDPOINT_URL"];
        var otel = builder.Services.AddOpenTelemetry();

        otel.ConfigureResource(resource => resource
            .AddService(builder.Environment.ApplicationName));

        otel.WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddMeter(MetricExtensions.SMeter.Name)
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddMeter("System.Net.Http")
            .AddMeter("System.Net.NameResolution")
            .AddPrometheusExporter());

        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddSource(MetricExtensions.OrderActivitySource.Name);
            if (tracingOtlpEndpoint != null)
            {
                tracing.AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint); });
            }
            else
            {
                tracing.AddConsoleExporter();
            }
        });
    }
}