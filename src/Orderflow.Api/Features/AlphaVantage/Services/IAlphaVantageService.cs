using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Features.Common;

namespace Orderflow.Features.AlphaVantage.Services;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}