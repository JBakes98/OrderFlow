using Microsoft.EntityFrameworkCore;
using Orderflow.Common.Repositories;

namespace Orderflow.Common.Extensions;

public static class PostgresExtensions
{
    public static void RegisterPostgres(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<OrderflowDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());
    }
}