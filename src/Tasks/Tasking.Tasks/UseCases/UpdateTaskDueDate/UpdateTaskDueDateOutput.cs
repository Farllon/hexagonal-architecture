namespace Tasking.Tasks.UseCases.UpdateTaskDueDate
{
    public record UpdateTaskDueDateOutput(Guid Id, string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
