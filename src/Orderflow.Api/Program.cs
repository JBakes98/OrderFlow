using Orderflow.Api.Authorization;
using Orderflow.Api.Routes.Instrument;
using Orderflow.Api.Routes.Order;
using Orderflow.Api.Swagger;
using Orderflow.Extensions;
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
builder.Services.RegisterAlphaVantage(config);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.AddCustomSwaggerGenOptions());

builder.Services.RegisterAuthentication(config);
builder.Services.AddAuthorization(opt => { opt.AddAuthorizationPolicies(); });

builder.Services.AddProblemDetails();

builder.Services.RegisterCors(config);

var app = builder.Build();

app.UseCors("OrderflowDashboard");
app.UseHttpsRedirection();
app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(opt => opt.AddCustomSwaggerUIOptions(app.Environment.IsDevelopment()));

app.UseSerilogRequestLogging();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.MapInstrumentUserGroup();
app.MapOrderUserGroup();

app.Run();