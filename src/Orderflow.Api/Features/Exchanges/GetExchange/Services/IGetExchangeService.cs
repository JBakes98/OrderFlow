using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;

namespace Orderflow.Features.Exchanges.GetExchange.Services;

public interface IGetExchangeService
{
    Task<OneOf<Exchange, Error>> GetExchangeById(Guid id);
}