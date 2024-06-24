using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace OrderFlow.Extensions;

public static class AuthenticationExtensions
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCognitoIdentity();

        var cognitoAppClientId = configuration["Cognito:AppClientId"];
        var cognitoUserPoolId = configuration["Cognito:UserPoolId"];
        var cognitoRegion = configuration["Cognito:Region"];

        var validIssuer = $"https://cognito-idp.{cognitoRegion}.amazonaws.com/{cognitoUserPoolId}";
        var validAudience = cognitoAppClientId;

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = validIssuer;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Cognito:Authority"],
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = "",
                    AudienceValidator = (audiences, securityToken, validationParameters) =>
                    {
                        var castedToken = securityToken as JsonWebToken;
                        var clientId = castedToken?.GetPayloadValue<string>("client_id")?.ToString();

                        return validAudience.Equals(clientId);
                    }
                };
            });
    }
}