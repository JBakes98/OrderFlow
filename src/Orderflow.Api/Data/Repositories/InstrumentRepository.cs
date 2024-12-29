using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Orderflow.Data.DbContext;
using Orderflow.Data.Entities;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Mappers;
using Serilog;

namespace Orderflow.Data.Repositories;

public class InstrumentRepository : IInstrumentRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IEventMapperFactory _eventMapperFactory;
    private readonly IMapper<InstrumentEntity, Instrument> _instrumentDataToDomainMapper;
    private readonly IMapper<Instrument, InstrumentEntity> _instrumentDomainToDataMapper;


    public InstrumentRepository(OrderflowDbContext context,
        IMapper<Instrument, InstrumentEntity> instrumentDomainToDataMapper,
        IMapper<InstrumentEntity, Instrument> instrumentDataToDomainMapper,
        IEventMapperFactory eventMapperFactory,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _eventMapperFactory = Guard.Against.Null(eventMapperFactory);
        _instrumentDataToDomainMapper = Guard.Against.Null(instrumentDataToDomainMapper);
        _instrumentDomainToDataMapper = Guard.Against.Null(instrumentDomainToDataMapper);
        _context = Guard.Against.Null(context);
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync()
    {
        var result = await _context.Instruments.ToListAsync();

        var instruments = result.Select(x =>
                _instrumentDataToDomainMapper.Map(x))
            .ToList();

        return instruments;
    }

    public async Task<OneOf<Instrument, Error>> GetByIdAsync(string id)
    {
        var instrumentEntity = await _context.Instruments.FindAsync(id);

        if (instrumentEntity == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        var instrument = _instrumentDataToDomainMapper.Map(instrumentEntity);

        return instrument;
    }

    public async Task<Error?> InsertAsync(Instrument source, InstrumentCreatedEvent @event)
    {
        var outboxEvent = _eventMapperFactory.MapEvent(@event);
        var entity = _instrumentDomainToDataMapper.Map(source);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Set<InstrumentEntity>().Add(entity);
            _context.Set<OutboxEvent>().Add(outboxEvent);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return null;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _diagnosticContext.Set("Instrument.Error", $"Failed to create instrument: {e.Message}");

            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.InstrumentCouldNotBeCreated);
        }
    }

    public Task<Error?> UpdateAsync(Instrument source)
    {
        throw new NotImplementedException();
    }
}