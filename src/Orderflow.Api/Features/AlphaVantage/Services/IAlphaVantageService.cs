using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.AlphaVantage.Models;

namespace Orderflow.Features.AlphaVantage.Services;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}