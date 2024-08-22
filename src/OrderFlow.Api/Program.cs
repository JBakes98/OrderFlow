using OrderFlow.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterLogging(builder.Configuration);

builder.Services.RegisterAwsServices(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.RegisterSwaggerServices();
builder.Services.RegisterAlphaVantage(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();