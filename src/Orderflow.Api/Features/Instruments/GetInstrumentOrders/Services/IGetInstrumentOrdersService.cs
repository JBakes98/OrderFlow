using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Api.Routes.Instrument.GetInstrumentOrders.Services;

public interface IGetInstrumentOrdersService
{
    Task<OneOf<IEnumerable<Domain.Models.Order>, Error>> GetInstrumentOrders(string instrumentId);
}