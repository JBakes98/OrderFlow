using System.Net;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Domain;
using OrderFlow.Models;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Services;

public class InstrumentService : IInstrumentService
{
    private readonly AppDbContext _context;

    public InstrumentService(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<Instrument, Error>> RetrieveInstrument(string id)
    {
        var instrument = await _context.Instruments.FindAsync(id);
        
        if (instrument == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        return instrument;
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments()
    {
        var result = await _context.Instruments.ToListAsync();

        return result;
    }
}