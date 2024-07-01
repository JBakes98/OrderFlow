using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Services.AlphaVantage;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}