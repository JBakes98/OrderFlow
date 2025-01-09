using System.Net;
using Ardalis.GuardClauses;
using Orderflow.Data.DbContext;
using Orderflow.Data.Entities;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain;
using Orderflow.Domain.Models;
using Orderflow.Events.Factories;
using Orderflow.Events.Trade;
using Orderflow.Mappers;
using Serilog;

namespace Orderflow.Data.Repositories;

public class TradeRepository : ITradeRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IOutboxEventMapperFactory _outboxEventMapperFactory;
    private readonly IMapper<Trade, TradeEntity> _tradeDomainToEntityMapper;

    public TradeRepository(OrderflowDbContext context, IDiagnosticContext diagnosticContext,
        IOutboxEventMapperFactory outboxEventMapperFactory, IMapper<Trade, TradeEntity> tradeDomaintToEntityMapper)
    {
        _tradeDomainToEntityMapper = Guard.Against.Null(tradeDomaintToEntityMapper);
        _context = Guard.Against.Null(context);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _outboxEventMapperFactory = Guard.Against.Null(outboxEventMapperFactory);
    }

    public async Task<Error?> InsertAsync(Trade trade, TradeExecutedEvent @event)
    {
        var outboxEvent = _outboxEventMapperFactory.MapEvent(@event);
        var entity = _tradeDomainToEntityMapper.Map(trade);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Set<TradeEntity>().Add(entity);
            _context.Set<OutboxEvent>().Add(outboxEvent);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return null;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _diagnosticContext.Set("Trade.Error", $"Failed to execute trade: {e.Message}");
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.TradeExecutionFailed);
        }
    }
}