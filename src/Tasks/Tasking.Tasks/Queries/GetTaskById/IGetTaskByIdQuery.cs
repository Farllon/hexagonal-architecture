namespace Tasking.Tasks.Queries.GetTaskById
{
    public interface IGetTaskByIdQuery : IDisposable
    {
        Task<GetTaskByIdOutput?> ExecuteAsync(Guid taskId, CancellationToken cancellationToken);
    }
}
