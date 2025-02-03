using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Instruments.Common.Models;

namespace Orderflow.Features.Instruments.GetInstrument.Services;

public interface IGetInstrumentService
{
    Task<OneOf<Instrument, Error>> GetInstrument(Guid id);
}