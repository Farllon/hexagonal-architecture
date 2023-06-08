namespace Tasking.Tasks.Queries.GetTaskById
{
    public record GetTaskByIdOutput(Guid Id, string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
