using OrderFlow.Options;

namespace OrderFlow.Extensions;

public static class CorsExtensions
{
    public static void RegisterCors(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var dashboardConfig = new DashboardOptions();
        configuration.GetSection("Dashboard").Bind(dashboardConfig);

        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(dashboardConfig.Name, policy =>
            {
                policy.WithOrigins(dashboardConfig.Origin)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}