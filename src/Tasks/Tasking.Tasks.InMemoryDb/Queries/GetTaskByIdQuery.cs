using Tasking.Tasks.Queries.GetTaskById;

namespace Tasking.Tasks.InMemoryDb.Queries
{
    internal class GetTaskByIdQuery : IGetTaskByIdQuery
    {
        private readonly MemoryDb _db;

        public GetTaskByIdQuery(MemoryDb db)
        {
            _db = db;
        }

        public Task<GetTaskByIdOutput?> ExecuteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            _db.Tasks.TryGetValue(taskId, out var task);

            if (task is null)
                return Task.FromResult<GetTaskByIdOutput?>(default);

            return Task.FromResult<GetTaskByIdOutput?>(new GetTaskByIdOutput(
                task.Id,
                task.Title.Value,
                task.Description?.Value,
                task.Status,
                task.DueDate.Value));
        }

        public void Dispose() { }
    }
}
