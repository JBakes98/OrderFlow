using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Domain.Models;
using OrderFlow.Options;
using Serilog;
using GlobalQuote = OrderFlow.Contracts.Responses.AlphaVantage.GlobalQuote;

namespace OrderFlow.Services.AlphaVantage;

public class AlphaVantageService(
    IHttpClientFactory httpClientFactory,
    IOptions<AlphaVantageOptions> options,
    IDiagnosticContext diagnosticContext)
    : IAlphaVantageService
{
    private readonly IHttpClientFactory _httpClientFactory = Guard.Against.Null(httpClientFactory);
    private readonly AlphaVantageOptions _options = Guard.Against.Null(options.Value);
    private readonly IDiagnosticContext _diagnosticContext = Guard.Against.Null(diagnosticContext);


    public async Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(_options.ClientName);

            var response =
                await client.GetStringAsync(
                    $"query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_options.ApiKey}");

            var jsonObject = JsonNode.Parse(response)?["Global Quote"];
            var quoteResponse = jsonObject.Deserialize<GlobalQuote>();

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