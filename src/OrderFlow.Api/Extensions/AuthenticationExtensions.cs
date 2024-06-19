using Microsoft.AspNetCore.Identity;
using OrderFlow.Contexts;
using OrderFlow.Models;

namespace OrderFlow.Extensions;

public static class AuthenticationExtensions
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<User>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddApiEndpoints();
    }
}