namespace Tasking.Tasks.UseCases.UpdateTask
{
    public interface IUpdateTaskUseCase : IDisposable
    {
        Task<UpdateTaskOutput> ExecuteAsync(UpdateTaskInput input, CancellationToken cancellationToken);
    }
}
