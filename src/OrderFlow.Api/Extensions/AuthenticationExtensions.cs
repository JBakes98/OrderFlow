using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using OrderFlow.Services;
using OrderFlow.Services.Handlers;

namespace OrderFlow.Extensions;

public static class AuthenticationExtensions
{
    public static void RegisterAuthentication(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        
    }
}