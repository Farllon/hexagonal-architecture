using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasking.Shared
{
    public interface IRepository<TAggregate> : IDisposable
        where TAggregate : class, IAggregateRoot
    {
        Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken);

        Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken);

        Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken);
    }
}
