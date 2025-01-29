using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.CreateExchange.Events;

namespace Orderflow.Features.Exchanges.Common.Repositories;

public interface IExchangeRepository
{
    Task<OneOf<IEnumerable<Exchange>, Error>> QueryAsync();
    Task<OneOf<Exchange, Error>> GetByIdAsync(Guid id);
    Task<Error?> InsertAsync(Exchange entity, ExchangeCreatedEvent @event);
    Task<Error?> UpdateAsync(Exchange source);
}