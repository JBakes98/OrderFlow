using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;

namespace Orderflow.Features.Exchanges.CreateExchange.Services;

public interface ICreateExchangeService
{
    Task<OneOf<Exchange, Error>> CreateExchange(Exchange instrument);
}