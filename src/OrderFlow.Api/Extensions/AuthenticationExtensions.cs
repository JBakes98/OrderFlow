using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OrderFlow.Extensions;

public static class AuthenticationExtensions
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCognitoIdentity();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Cognito:Authority"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Cognito:Authority"],
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = "",
                    ValidateAudience = false
                };
            });
    }
}