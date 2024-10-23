using OneOf;
using OrderFlow.Domain.Models;

namespace OrderFlow.Services.Handlers;

public interface IHandler<in TRequest, T>
{
    Task<OneOf<T, Error>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}