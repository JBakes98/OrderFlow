using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common;

namespace Orderflow.Features.Exchanges.CreateExchange.Services;

public interface ICreateExchangeService
{
    Task<OneOf<Exchange, Error>> CreateExchange(Exchange instrument);
}