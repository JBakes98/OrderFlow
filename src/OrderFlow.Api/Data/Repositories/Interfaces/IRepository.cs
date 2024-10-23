using OneOf;
using OrderFlow.Domain.Models;

namespace OrderFlow.Data.Repositories.Interfaces;

public interface IRepository<T> : IDisposable
{
    Task<OneOf<IEnumerable<T>, Error>> QueryAsync();
    Task<OneOf<T, Error>> GetByIdAsync(string id);
    Task<OneOf<T, Error>> InsertAsync(T source, CancellationToken cancellationToken);
    Task DeleteAsync(T source);
    Task UpdateAsync(T source);
}