namespace Tasking.Tasks.UseCases.CreateTask
{
    public record CreateTaskInput(string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
