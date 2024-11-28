using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.AlphaVantage;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}