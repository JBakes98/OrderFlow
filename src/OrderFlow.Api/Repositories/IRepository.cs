using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public interface IRepository<T> : IDisposable
{
    Task<IEnumerable<T>> Get();
    Task<OneOf<T, Error>> GetById(string id);
    Task Insert(T source, CancellationToken cancellationToken);
    Task Delete(T source); 
    Task Update(T source);
}