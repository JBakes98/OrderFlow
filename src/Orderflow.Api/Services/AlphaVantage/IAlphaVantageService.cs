using OneOf;
using Orderflow.Domain.Models;
using GlobalQuote = Orderflow.Contracts.Responses.AlphaVantage.GlobalQuote;

namespace Orderflow.Services.AlphaVantage;

public interface IAlphaVantageService
{
    Task<OneOf<GlobalQuote, Error>> GetStockQuote(string symbol);
}