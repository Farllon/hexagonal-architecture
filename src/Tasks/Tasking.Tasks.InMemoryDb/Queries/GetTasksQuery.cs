using Tasking.Tasks.Queries.GetTasks;

namespace Tasking.Tasks.InMemoryDb.Queries
{
    internal class GetTasksQuery : IGetTasksQuery
    {
        private readonly MemoryDb _db;

        public GetTasksQuery(MemoryDb db)
        {
            _db = db;
        }

        public Task<GetTasksOutput> ExecuteAsync(GetTasksInput input, CancellationToken cancellationToken)
        {
            if (input.Page < IGetTasksQuery.MinPage)
                throw Errors.TaskQueriesErrors.InvalidPage;

            if (input.Size < IGetTasksQuery.MinSize || input.Size > IGetTasksQuery.MaxSize)
                throw Errors.TaskQueriesErrors.InvalidPageSize;

            var tasks = _db.Tasks.Values
                .Skip((input.Page - 1) * input.Size)
                .Take(input.Size)
                .Select(task => new TaskDTO(
                    task.Id,
                    task.Title.Value,
                    task.Description?.Value,
                    task.Status,
                    task.DueDate.Value))
                .ToList();

            var count = _db.Tasks.Count;

            var totalPages = count > 0
                ? (int)Math.Ceiling((double)count / tasks.Count)
                : 1;

            return Task.FromResult(new GetTasksOutput(
                tasks, 
                input.Page, 
                count, 
                totalPages));
        }

        public void Dispose() { }
    }
}
