using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Orders.Common.Models;

namespace Orderflow.Features.Instruments.GetInstrumentOrders.Services;

public interface IGetInstrumentOrdersService
{
    Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId);
}