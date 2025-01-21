using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface IExchangeService
{
    Task<OneOf<Exchange, Error>> GetExchangeById(Guid id);
    Task<OneOf<IEnumerable<Exchange>, Error>> GetExchanges();
    Task<OneOf<Exchange, Error>> CreateExchange(Exchange instrument);
}