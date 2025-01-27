using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;

namespace Orderflow.Features.Instruments.CreateInstrument.Services;

public interface ICreateInstrumentService
{
    Task<OneOf<Instrument, Error>> CreateInstrument(Instrument instrument);
}