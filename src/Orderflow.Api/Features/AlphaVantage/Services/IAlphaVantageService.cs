using OneOf;
using Orderflow.Features.AlphaVantage.Models;
using Orderflow.Features.Common.Models;

namespace Orderflow.Features.AlphaVantage.Services;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}