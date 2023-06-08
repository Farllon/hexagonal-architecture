namespace Tasking.Tasks.UseCases.CreateTask
{
    public interface ICreateTaskUseCase : IDisposable
    {
        Task<CreateTaskOutput> ExecuteAsync(CreateTaskInput input, CancellationToken cancellationToken);
    }
}
