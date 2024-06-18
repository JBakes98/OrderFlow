using OrderFlow.Extensions;
using OrderFlow.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterAwsServices(builder.Configuration);
builder.Services.RegisterDatabase(builder.Configuration);
builder.RegisterLogging(builder.Configuration);
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.AddControllers();

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

app.MapIdentityApi<User>();
app.MapControllers();

app.Run();

