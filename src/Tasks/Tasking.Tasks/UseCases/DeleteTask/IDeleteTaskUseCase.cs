namespace Tasking.Tasks.UseCases.DeleteTask
{
    public interface IDeleteTaskUseCase : IDisposable
    {
        Task ExecuteAsync(Guid taskId, CancellationToken cancellationToken);
    }
}
