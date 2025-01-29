using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Instruments.Common.Models;

namespace Orderflow.Features.Instruments.CreateInstrument.Services;

public interface ICreateInstrumentService
{
    Task<OneOf<Instrument, Error>> CreateInstrument(Instrument instrument);
}