namespace Tasking.Tasks.Queries.GetTasks
{
    public interface IGetTasksQuery : IDisposable
    {
        public const int MinPage = 1;

        public const int MinSize = 1;

        public const int MaxSize = 30;

        Task<GetTasksOutput> ExecuteAsync(GetTasksInput input, CancellationToken cancellationToken);
    }
}
