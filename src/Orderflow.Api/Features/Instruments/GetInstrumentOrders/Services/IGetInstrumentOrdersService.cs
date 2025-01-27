using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Features.Instruments.GetInstrumentOrders.Services;

public interface IGetInstrumentOrdersService
{
    Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId);
}