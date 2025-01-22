using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Serilog;

namespace Orderflow.Api.Routes.Instrument.GetInstrumentOrders.Services;

public class GetInstrumentOrdersService : IGetInstrumentOrdersService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IOrderRepository _repository;

    public GetInstrumentOrdersService(
        IOrderRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<IEnumerable<Domain.Models.Order>, Error>> GetInstrumentOrders(string instrumentId)
    {
        var result = await _repository.GetInstrumentOrders(instrumentId);

        if (result.IsT1)
            return result.AsT1;

        return result;
    }
}