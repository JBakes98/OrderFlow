using Microsoft.EntityFrameworkCore;
using Orderflow.Data.DbContext;

namespace Orderflow.Extensions;

public static class PostgresExtensions
{
    public static void RegisterPostgres(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<OrderflowDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}