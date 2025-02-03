using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Orderflow.Middleware;

/// <summary>
///     This is the attribute class, which allows you to decorate endpoints with a [CustomScopeAuthorization] attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CustomScopeAuthorizationAttribute : TypeFilterAttribute
{
    public CustomScopeAuthorizationAttribute(params string[] requiredScopes) : base(
        typeof(CustomScopeAuthorizationFilter))
    {
        Arguments = [requiredScopes];
    }
}

/// <summary>
///     This is the code that performs the authorization.
/// </summary>
public class CustomScopeAuthorizationFilter : IAuthorizationFilter
{
    private readonly string[] _requiredScopes;

    public CustomScopeAuthorizationFilter(string[] requiredScopes)
    {
        _requiredScopes = Guard.Against.Null(requiredScopes);
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        // Check if the user is authenticated
        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Check if the required scopes are present in the token
        var scopeClaim = user.Claims.FirstOrDefault(item => item.Type == "scope");

        var tokenScopes = scopeClaim?.Value.Trim().Split(" ").ToList() ?? [];

        if (!_requiredScopes.All(scope => tokenScopes.Contains(scope))) context.Result = new ForbidResult();
    }
}