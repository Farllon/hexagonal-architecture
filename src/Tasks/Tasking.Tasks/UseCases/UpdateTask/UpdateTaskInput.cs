namespace Tasking.Tasks.UseCases.UpdateTask
{
    public record UpdateTaskInput(Guid TaskId, string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status);
}
