using System.Text;

namespace Orderflow.Extensions;

public static class ConfigurationExtensions
{
    public static string Dump(this IConfiguration configuration)
    {
        var log = new StringBuilder();

        log.Append("Configuration");
        log.AppendLine();

        foreach (var child in configuration.GetChildren())
            DumpSection(child, log, 0, true);

        return log.ToString();
    }

    private static void DumpSection(IConfigurationSection section, StringBuilder log, int depth,
        bool rootSection = false)
    {
        log.Append("\t");
        log.Append(' ', depth * 2);
        log.Append($"{section.Key}: {section.Value}\n");

        foreach (var child in section.GetChildren())
            DumpSection(child, log, depth + 1);

        if (!rootSection)
            return;

        log.AppendLine();
    }
}