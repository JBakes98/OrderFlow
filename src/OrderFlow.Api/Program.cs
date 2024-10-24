using OrderFlow.Extensions;
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
builder.Services.RegisterAuthentication(config);
builder.Services.AddControllers();
builder.Services.RegisterCors(config);
builder.Services.RegisterSwaggerServices();
builder.Services.RegisterAlphaVantage(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseCors("OrderflowDashboard");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();