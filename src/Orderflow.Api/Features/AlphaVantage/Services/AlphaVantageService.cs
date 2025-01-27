using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Features.AlphaVantage.Contracts;
using Orderflow.Features.Common;
using Orderflow.Options;
using Serilog;

namespace Orderflow.Features.AlphaVantage.Services;

public class AlphaVantageService(
    IHttpClientFactory httpClientFactory,
    IOptions<AlphaVantageOptions> options,
    IDiagnosticContext diagnosticContext,
    IMapper<GetGlobalQuoteResponse, GlobalQuote> globalQuoteMapper)
    : IAlphaVantageService
{
    private readonly IDiagnosticContext _diagnosticContext = Guard.Against.Null(diagnosticContext);
    private readonly IHttpClientFactory _httpClientFactory = Guard.Against.Null(httpClientFactory);
    private readonly AlphaVantageOptions _options = Guard.Against.Null(options.Value);

    private readonly IMapper<GetGlobalQuoteResponse, GlobalQuote> _globalQuoteMapper =
        Guard.Against.Null(globalQuoteMapper);

    public async Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol)
    {
        var httpResponse = await SendHttpRequest(symbol);

        if (httpResponse.TryPickT1(out var error, out var quote))
            return error;

        _diagnosticContext.Set("AlphaVantage:QuoteResponse", quote, true);

        return _globalQuoteMapper.Map(quote);
    }

    private async Task<OneOf<GetGlobalQuoteResponse, Error>> SendHttpRequest(string symbol)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(_options.ClientName);

            var response =
                await client.GetStringAsync(
                    $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_options.ApiKey}");

            var jsonObject = JsonNode.Parse(response)?["Global Quote"];
            var quoteResponse = jsonObject.Deserialize<GetGlobalQuoteResponse>();

            if (quoteResponse == null)
            {
                _diagnosticContext.Set("AlphaVantage:ErrorMessage", "Alpha Vantage quote null");
                return new Error(HttpStatusCode.ServiceUnavailable, ErrorCodes.UnableToRetrieveCurrentInstrumentPrice);
            }

            return quoteResponse;
        }
        catch (Exception e)
        {
            _diagnosticContext.Set("AlphaVantage:ErrorMessage", e.Message);
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.UnableToRetrieveCurrentInstrumentPrice);
        }
    }
}