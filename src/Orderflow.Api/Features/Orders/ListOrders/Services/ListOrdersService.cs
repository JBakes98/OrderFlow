using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;
using Orderflow.Features.Orders.Common.Repositories;
using Serilog;

namespace Orderflow.Features.Orders.ListOrders.Services;

public class ListOrdersService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IOrderRepository _repository;

    public ListOrdersService(
        IOrderRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> ListOrders()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0.ToList();
    }
}