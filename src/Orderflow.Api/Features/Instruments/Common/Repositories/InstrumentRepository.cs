using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Orderflow.Common.Events.Factories;
using Orderflow.Common.Mappers;
using Orderflow.Common.Models;
using Orderflow.Common.Repositories;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.CreateInstrument.Events;
using Serilog;

namespace Orderflow.Features.Instruments.Common.Repositories;

public class InstrumentRepository : IInstrumentRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IOutboxEventMapperFactory _outboxEventMapperFactory;
    private readonly IMapper<InstrumentEntity, Instrument> _instrumentDataToDomainMapper;
    private readonly IMapper<Instrument, InstrumentEntity> _instrumentDomainToDataMapper;


    public InstrumentRepository(OrderflowDbContext context,
        IMapper<Instrument, InstrumentEntity> instrumentDomainToDataMapper,
        IMapper<InstrumentEntity, Instrument> instrumentDataToDomainMapper,
        IOutboxEventMapperFactory outboxEventMapperFactory,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _outboxEventMapperFactory = Guard.Against.Null(outboxEventMapperFactory);
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

    public async Task<OneOf<Instrument, Error>> GetByIdAsync(Guid id)
    {
        var instrumentEntity = await _context.Instruments.FindAsync(id);

        if (instrumentEntity == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        var instrument = _instrumentDataToDomainMapper.Map(instrumentEntity);

        return instrument;
    }

    public async Task<Error?> InsertAsync(Instrument source, InstrumentCreatedEvent @event)
    {
        var outboxEvent = _outboxEventMapperFactory.MapEvent(@event);
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