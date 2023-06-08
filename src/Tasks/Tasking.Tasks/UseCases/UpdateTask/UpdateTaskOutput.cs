namespace Tasking.Tasks.UseCases.UpdateTask
{
    public record UpdateTaskOutput(Guid Id, string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
