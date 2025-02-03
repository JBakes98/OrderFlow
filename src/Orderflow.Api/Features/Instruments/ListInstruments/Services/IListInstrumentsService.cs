using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Instruments.Common.Models;

namespace Orderflow.Features.Instruments.ListInstruments.Services;

public interface IListInstrumentsService
{
    Task<OneOf<IEnumerable<Instrument>, Error>> ListInstruments();
}