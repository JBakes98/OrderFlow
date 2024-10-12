using Microsoft.Extensions.Configuration;

namespace OrderFlowHistoryFunction.Extensions;

public static class ConfigurationExtensions
{
    public static IConfiguration Build(string envVariablePrefix)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false)
            .AddEnvironmentVariables($"{envVariablePrefix}_")
            .Build();
    }
}