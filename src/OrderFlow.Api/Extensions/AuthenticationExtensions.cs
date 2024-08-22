using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OrderFlow.Options;

namespace OrderFlow.Extensions;

public static class AuthenticationExtensions
{
    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCognitoIdentity();

        var cognitoConfig = configuration.GetSection("Cognito").Get<CognitoOptions>();

        var cognitoAppClientId = cognitoConfig?.AppClientId;
        var validIssuer = $"https://cognito-idp.{cognitoConfig?.Region}.amazonaws.com/{cognitoConfig?.UserPoolId}";
        var validAudience = cognitoAppClientId;

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = validIssuer;
                options.RequireHttpsMetadata = cognitoConfig!.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = cognitoConfig.Authority,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = "",
                    AudienceValidator = (audiences, securityToken, validationParameters) =>
                    {
                        var castedToken = securityToken as JsonWebToken;
                        var clientId = castedToken?.GetPayloadValue<string>("client_id");

                        return validAudience!.Equals(clientId);
                    }
                };
            });
    }
}