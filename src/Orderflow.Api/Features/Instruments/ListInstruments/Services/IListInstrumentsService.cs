using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;

namespace Orderflow.Features.Instruments.ListInstruments.Services;

public interface IListInstrumentsService
{
    Task<OneOf<IEnumerable<Instrument>, Error>> ListInstruments();
}