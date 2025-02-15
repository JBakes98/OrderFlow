namespace Orderflow.Options;

public class DashboardOptions
{
    public string Origin { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool AllowAnyHeaders { get; set; } = false;
    public bool AllowAnyMethod { get; set; } = false;
    public bool AllowCredentials { get; set; } = false;
}