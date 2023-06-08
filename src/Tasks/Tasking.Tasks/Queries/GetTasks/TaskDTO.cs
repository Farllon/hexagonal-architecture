namespace Tasking.Tasks.Queries.GetTasks
{
    public record TaskDTO(Guid Id, string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
