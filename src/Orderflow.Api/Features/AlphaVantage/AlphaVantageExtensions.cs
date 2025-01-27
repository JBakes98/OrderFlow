using Orderflow.Features.AlphaVantage.Services;
using Orderflow.Options;

namespace Orderflow.Extensions;

public static class AlphaVantageExtensions
{
    public static void RegisterAlphaVantage(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var clientConfig = new AlphaVantageOptions();
        configuration.GetSection("AlphaVantage").Bind(clientConfig);

        serviceCollection.Configure<AlphaVantageOptions>(
            configuration.GetSection("AlphaVantage"));

        serviceCollection.AddHttpClient(
            clientConfig.ClientName,
            client => { client.BaseAddress = new Uri(clientConfig.BasePath); });

        serviceCollection.AddScoped<IAlphaVantageService, AlphaVantageService>();
    }
}