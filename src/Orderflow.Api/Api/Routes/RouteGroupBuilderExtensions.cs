using System.Diagnostics.CodeAnalysis;
using Orderflow.Api.Authorization;
using Orderflow.Api.Swagger;

namespace Orderflow.Api.Routes;

public static class RouteGroupBuilderExtensions
{
    public static RouteGroupBuilder MapUserGroup(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string prefix, string? groupTagName = null,
        string[]? extraRequiredPolicies = null)
    {
        extraRequiredPolicies ??= [];

        var group = endpoints.MapGroup(prefix).WithGroupName(SwaggerConfiguration.User);

        if (groupTagName != null)
            group.WithTags(groupTagName);

        group.WithOpenApi();

        return group;
    }

    public static RouteGroupBuilder MapAdminGroup(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string prefix, string? groupTagName = null,
        string[]? extraRequiredPolicies = null)
    {
        extraRequiredPolicies ??= [];

        var group = endpoints.MapGroup(prefix).WithGroupName(SwaggerConfiguration.Admin)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies]);

        if (groupTagName != null)
            group.WithTags(groupTagName);

        group.WithOpenApi();

        return group;
    }
}