using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Handlers;

public interface IHandler<in TRequest, T>
{
    Task<OneOf<T, Error>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}