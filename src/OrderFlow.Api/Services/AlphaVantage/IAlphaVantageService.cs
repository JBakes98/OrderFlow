using OneOf;
using OrderFlow.Domain.Models;
using GlobalQuote = OrderFlow.Contracts.Responses.AlphaVantage.GlobalQuote;

namespace OrderFlow.Services.AlphaVantage;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}