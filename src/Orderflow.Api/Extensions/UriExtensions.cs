namespace Orderflow.Extensions;

public static class UriExtensions
{
    public static string GenerateUri(HttpContext context, string route, object id)
    {
        var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
        return $"{baseUrl}/{route}/{id}";
    }
}