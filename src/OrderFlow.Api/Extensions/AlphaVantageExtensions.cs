using OrderFlow.Options;
using OrderFlow.Services.AlphaVantage;

namespace OrderFlow.Extensions;

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

        serviceCollection.AddSingleton<IAlphaVantageService, AlphaVantageService>();
    }
}