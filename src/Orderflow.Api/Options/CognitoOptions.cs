namespace Orderflow.Options;

public class CognitoOptions
{
    public string Authority { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string AppClientId { get; set; } = string.Empty;
    public string UserPoolId { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; } = true;
}