using Tasking.Tasks.Aggregates.TaskAggregate;

namespace Tasking.Tasks.InMemoryDb.Repositories
{
    internal sealed class TaskRepository : ITaskRepository
    {
        private readonly MemoryDb _db;

        public TaskRepository(MemoryDb db)
        {
            _db = db;
        }

        public Task<Aggregates.TaskAggregate.Task?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _db.Tasks.TryGetValue(id, out var task);

            return System.Threading.Tasks.Task.FromResult(task);
        }
        
        public System.Threading.Tasks.Task CreateAsync(Aggregates.TaskAggregate.Task aggregate, CancellationToken cancellationToken)
        {
            _db.Tasks.Add(aggregate.Id, aggregate);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task UpdateAsync(Aggregates.TaskAggregate.Task aggregate, CancellationToken cancellationToken)
        {
            _db.Tasks[aggregate.Id] = aggregate;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task DeleteAsync(Aggregates.TaskAggregate.Task aggregate, CancellationToken cancellationToken)
        {
            _db.Tasks.Remove(aggregate.Id);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public void Dispose() { }
    }
}
