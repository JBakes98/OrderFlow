using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;

namespace Orderflow.Features.Instruments.GetInstrument.Services;

public interface IGetInstrumentService
{
    Task<OneOf<Instrument, Error>> GetInstrument(Guid id);
}