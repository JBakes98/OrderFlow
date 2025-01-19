using System.Diagnostics.CodeAnalysis;
using Orderflow.Api.Authorization;

namespace Orderflow.Api.Routes;

public static class RouteGroupBuilderExtensions
{
    public const string User = nameof(User);
    public const string Admin = nameof(Admin);

    public static RouteGroupBuilder MapUserGroup(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string prefix, string? groupTagName = null,
        string[]? extraRequiredPolicies = null)
    {
        extraRequiredPolicies ??= [];

        var group = endpoints.MapGroup(prefix);
        // .WithGroupName(User);

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

        var group = endpoints.MapGroup(prefix).WithGroupName(Admin)
            .RequireAuthorization([AuthorizationPolicy.Admin, .. extraRequiredPolicies]);

        if (groupTagName != null)
            group.WithTags(groupTagName);

        group.MapOpenApi();

        return group;
    }
}