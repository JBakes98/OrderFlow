using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Events.Exchange;

namespace Orderflow.Data.Repositories.Interfaces;

public interface IExchangeRepository
{
    Task<OneOf<IEnumerable<Exchange>, Error>> QueryAsync();
    Task<OneOf<Exchange, Error>> GetByIdAsync(Guid id);
    Task<Error?> InsertAsync(Exchange entity, ExchangeCreatedEvent @event);
    Task<Error?> UpdateAsync(Exchange source);
}