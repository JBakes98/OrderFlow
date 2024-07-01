using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Options;

namespace OrderFlow.Services.AlphaVantage;

public class AlphaVantageService : IAlphaVantageService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AlphaVantageOptions _options;
    private readonly IMapper<Contracts.Responses.AlphaVantage.GlobalQuote, GlobalQuote> _globalQuoteMapper;

    public AlphaVantageService(
        IHttpClientFactory httpClientFactory,
        IOptions<AlphaVantageOptions> options,
        IMapper<Contracts.Responses.AlphaVantage.GlobalQuote, GlobalQuote> globalQuoteMapper)
    {
        _globalQuoteMapper = Guard.Against.Null(globalQuoteMapper);
        _options = Guard.Against.Null(options.Value);
        _httpClientFactory = Guard.Against.Null(httpClientFactory);
    }


    public async Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_options.BasePath);

        var response =
            await client.GetStringAsync(
                $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_options.ApiKey}");

        var jsonObject = JsonNode.Parse(response)?["Global Quote"];
        var quoteResponse = jsonObject.Deserialize<Contracts.Responses.AlphaVantage.GlobalQuote>();

        if (quoteResponse == null)
            return new Error(HttpStatusCode.ServiceUnavailable, ErrorCodes.UnableToRetrieveCurrentInstrumentPrice);

        return _globalQuoteMapper.Map(quoteResponse);
    }
}