namespace OrderFlow.Options;

public class S3Options
{
    public string ServiceUrl { get; set; } = string.Empty;
    public bool UseHttp { get; set; } = false;
    public bool ForcePathStyle { get; set; } = false;
    public string AuthenticationRegion { get; set; } = string.Empty;
}