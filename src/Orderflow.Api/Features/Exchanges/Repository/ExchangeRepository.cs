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
using Orderflow.Events.Factories;
using Orderflow.Events.Exchange;
using Orderflow.Mappers;
using Serilog;

namespace Orderflow.Data.Repositories;

public class ExchangeRepository : IExchangeRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IOutboxEventMapperFactory _outboxEventMapperFactory;
    private readonly IMapper<ExchangeEntity, Exchange> _exchangeDataToDomainMapper;
    private readonly IMapper<Exchange, ExchangeEntity> _exchangeDomainToDataMapper;


    public ExchangeRepository(OrderflowDbContext context,
        IMapper<Exchange, ExchangeEntity> exchangeDomainToDataMapper,
        IMapper<ExchangeEntity, Exchange> exchangeDataToDomainMapper,
        IOutboxEventMapperFactory outboxEventMapperFactory,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _outboxEventMapperFactory = Guard.Against.Null(outboxEventMapperFactory);
        _exchangeDataToDomainMapper = Guard.Against.Null(exchangeDataToDomainMapper);
        _exchangeDomainToDataMapper = Guard.Against.Null(exchangeDomainToDataMapper);
        _context = Guard.Against.Null(context);
    }

    public async Task<OneOf<IEnumerable<Exchange>, Error>> QueryAsync()
    {
        var result = await _context.Exchanges.ToListAsync();

        var exchanges = result.Select(x =>
                _exchangeDataToDomainMapper.Map(x))
            .ToList();

        return exchanges;
    }

    public async Task<OneOf<Exchange, Error>> GetByIdAsync(Guid id)
    {
        var exchangeEntity = await _context.Exchanges.FindAsync(id);

        if (exchangeEntity == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.ExchangeNotFound);

        var exchange = _exchangeDataToDomainMapper.Map(exchangeEntity);

        return exchange;
    }

    public async Task<Error?> InsertAsync(Exchange source, ExchangeCreatedEvent @event)
    {
        var outboxEvent = _outboxEventMapperFactory.MapEvent(@event);
        var entity = _exchangeDomainToDataMapper.Map(source);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Set<ExchangeEntity>().Add(entity);
            _context.Set<OutboxEvent>().Add(outboxEvent);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return null;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _diagnosticContext.Set("Exchange.Error", $"Failed to create exchange: {e.Message}");

            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.ExchangeCouldNotBeCreated);
        }
    }

    public Task<Error?> UpdateAsync(Exchange source)
    {
        throw new NotImplementedException();
    }
}