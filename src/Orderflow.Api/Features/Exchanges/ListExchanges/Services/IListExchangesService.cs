using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;

namespace Orderflow.Features.Exchanges.ListExchanges.Services;

public interface IListExchangesService
{
    Task<OneOf<IEnumerable<Exchange>, Error>> ListExchanges();
}