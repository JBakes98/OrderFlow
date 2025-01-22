using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Features.Exchanges.ListExchanges.Services;

public interface IListExchangesService
{
    Task<OneOf<IEnumerable<Features.Exchanges.Common.Exchange>, Error>> ListExchanges();
}