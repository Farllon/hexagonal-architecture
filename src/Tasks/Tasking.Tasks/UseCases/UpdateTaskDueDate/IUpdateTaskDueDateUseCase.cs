namespace Tasking.Tasks.UseCases.UpdateTaskDueDate
{
    public interface IUpdateTaskDueDateUseCase : IDisposable
    {
        Task<UpdateTaskDueDateOutput> ExecuteAsync(UpdateTaskDueDateInput input, CancellationToken cancellationToken);
    }
}
