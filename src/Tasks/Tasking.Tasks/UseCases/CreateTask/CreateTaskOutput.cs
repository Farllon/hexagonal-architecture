namespace Tasking.Tasks.UseCases.CreateTask
{
    public record CreateTaskOutput(Guid Id, string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
