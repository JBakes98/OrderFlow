using System.Text.Json.Serialization;
using Orderflow.Api.Authorization;
using Orderflow.Extensions;
using Orderflow.Features.Exchanges;
using Orderflow.Features.Instruments;
using Orderflow.Features.Orders;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false)
    .AddEnvironmentVariables("ORDERFLOW_");

var config = configBuilder.Build();

builder.RegisterLogging(config);

builder.Services.RegisterAwsServices(config);
builder.Services.RegisterPostgres(config);
builder.Services.RegisterServices(config);
builder.Services.RegisterValidators();
builder.Services.RegisterAlphaVantage(config);

builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterAuthentication(config);
builder.Services.AddAuthorization(opt => { opt.AddAuthorizationPolicies(); });

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddProblemDetails();

builder.Services.RegisterCors(config);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("OrderflowDashboard");
app.UseHttpsRedirection();
app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.MapExchangeUserGroup();
app.MapInstrumentUserGroup();
app.MapOrderUserGroup();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.Run();