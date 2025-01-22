using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Features.Exchanges.GetExchange.Services;

public interface IGetExchangeService
{
    Task<OneOf<Exchange, Error>> GetExchangeById(Guid id);
    Task<OneOf<IEnumerable<Exchange>, Error>> GetExchanges();
}