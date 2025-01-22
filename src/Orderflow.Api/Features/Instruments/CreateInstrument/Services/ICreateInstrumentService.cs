using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface ICreateInstrumentService
{
    Task<OneOf<Instrument, Error>> CreateInstrument(Instrument instrument);
}