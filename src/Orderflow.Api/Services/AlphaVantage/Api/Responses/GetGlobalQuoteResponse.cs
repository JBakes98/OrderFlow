using System.Text.Json.Serialization;

namespace Orderflow.Services.AlphaVantage.Api.Responses;

public class GetGlobalQuoteResponse
{
    [JsonPropertyName("01. symbol")] public string Symbol { get; set; }
    [JsonPropertyName("02. open")] public string Open { get; set; }
    [JsonPropertyName("03. high")] public string High { get; set; }
    [JsonPropertyName("04. low")] public string Low { get; set; }
    [JsonPropertyName("05. price")] public string Price { get; set; }
    [JsonPropertyName("06. volume")] public string Volume { get; set; }

    [JsonPropertyName("07. latest trading day")]
    public string LatestTradingDate { get; set; }

    [JsonPropertyName("08. previous close")]
    public string PrevClose { get; set; }

    [JsonPropertyName("09. change")] public string Change { get; set; }

    [JsonPropertyName("10. change percent")]
    public string ChangePerc { get; set; }
}