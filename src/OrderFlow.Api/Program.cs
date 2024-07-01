using OrderFlow.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterAwsServices(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.RegisterServices(builder.Configuration);
builder.RegisterLogging(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterOptions(builder.Configuration);
builder.Services.AddControllers();
builder.Services.RegisterSwaggerServices();

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