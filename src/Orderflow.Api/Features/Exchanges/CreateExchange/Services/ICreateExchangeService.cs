using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Features.Exchanges.Common;

namespace Orderflow.Services.Interfaces;

public interface ICreateExchangeService
{
    Task<OneOf<Exchange, Error>> CreateExchange(Exchange instrument);
}