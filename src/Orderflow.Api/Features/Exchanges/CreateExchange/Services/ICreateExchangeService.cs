using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface ICreateExchangeService
{
    Task<OneOf<Exchange, Error>> CreateExchange(Exchange instrument);
}