using Microsoft.EntityFrameworkCore;
using OrderFlow.Contexts;

namespace OrderFlow.Extensions;

public static class DatabaseExtensions
{
    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => 
                    x.MigrationsHistoryTable("_EfMigrations", 
                        configuration.GetSection("Schema")
                            .GetSection("<YourDataSchema").Value)));
    }
}