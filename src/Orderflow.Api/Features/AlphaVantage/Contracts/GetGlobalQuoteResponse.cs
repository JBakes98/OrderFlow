using System.Text.Json.Serialization;

namespace Orderflow.Features.AlphaVantage.Contracts;

public class GetGlobalQuoteResponse
{
    [JsonPropertyName("01. symbol")] public required string Symbol { get; set; }
    [JsonPropertyName("02. open")] public required string Open { get; set; }
    [JsonPropertyName("03. high")] public required string High { get; set; }
    [JsonPropertyName("04. low")] public required string Low { get; set; }
    [JsonPropertyName("05. price")] public required string Price { get; set; }
    [JsonPropertyName("06. volume")] public required string Volume { get; set; }

    [JsonPropertyName("07. latest trading day")]
    public required string LatestTradingDate { get; set; }

    [JsonPropertyName("08. previous close")]
    public required string PrevClose { get; set; }

    [JsonPropertyName("09. change")] public required string Change { get; set; }

    [JsonPropertyName("10. change percent")]
    public required string ChangePerc { get; set; }
}