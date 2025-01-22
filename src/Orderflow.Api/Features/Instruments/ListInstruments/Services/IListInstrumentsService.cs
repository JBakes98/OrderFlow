using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Api.Routes.Instrument.ListInstruments.Services;

public interface IListInstrumentsService
{
    Task<OneOf<IEnumerable<Domain.Models.Instrument>, Error>> ListInstruments();
}