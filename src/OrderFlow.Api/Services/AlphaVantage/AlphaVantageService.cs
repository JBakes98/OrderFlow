using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Models;
using OrderFlow.Options;
using GlobalQuote = OrderFlow.Contracts.Responses.AlphaVantage.GlobalQuote;

namespace OrderFlow.Services.AlphaVantage;

public class AlphaVantageService : IAlphaVantageService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AlphaVantageOptions _options;

    public AlphaVantageService(
        IHttpClientFactory httpClientFactory,
        IOptions<AlphaVantageOptions> options)
    {
        _options = Guard.Against.Null(options.Value);
        _httpClientFactory = Guard.Against.Null(httpClientFactory);
    }


    public async Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol)
    {
        var client = _httpClientFactory.CreateClient(_options.ClientName);

        var response =
            await client.GetStringAsync(
                $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_options.ApiKey}");

        var jsonObject = JsonNode.Parse(response)?["Global Quote"];
        var quoteResponse = jsonObject.Deserialize<GlobalQuote>();

        if (quoteResponse == null)
            return new Error(HttpStatusCode.ServiceUnavailable, ErrorCodes.UnableToRetrieveCurrentInstrumentPrice);

        return quoteResponse;
    }
}