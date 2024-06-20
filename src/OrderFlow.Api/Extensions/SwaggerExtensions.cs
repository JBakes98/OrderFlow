namespace OrderFlow.Extensions;

public static class SwaggerExtensions
{
    public static void RegisterSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}