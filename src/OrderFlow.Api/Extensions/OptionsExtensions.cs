using OrderFlow.Options;

namespace OrderFlow.Extensions;

public static class OptionsExtensions
{
    public static void RegisterOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<AlphaVantageOptions>(
            configuration.GetSection("AlphaVantage"));
    }
}