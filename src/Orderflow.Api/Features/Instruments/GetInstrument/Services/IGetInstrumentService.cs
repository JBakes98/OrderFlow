using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Api.Routes.Instrument.GetInstrument.Services;

public interface IGetInstrumentService
{
    Task<OneOf<Domain.Models.Instrument, Error>> GetInstrument(Guid id);
}