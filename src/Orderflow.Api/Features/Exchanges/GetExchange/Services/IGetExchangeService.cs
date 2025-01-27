using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common;

namespace Orderflow.Features.Exchanges.GetExchange.Services;

public interface IGetExchangeService
{
    Task<OneOf<Exchange, Error>> GetExchangeById(Guid id);
}