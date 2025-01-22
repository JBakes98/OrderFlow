using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Serilog;

namespace Orderflow.Api.Routes.Order.GetOrder.Services;

public class GetOrderService : IGetOrderService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IOrderRepository _repository;

    public GetOrderService(
        IOrderRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }


    public async Task<OneOf<Domain.Models.Order, Error>> GetOrder(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }
}