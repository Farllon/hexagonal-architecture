namespace Tasking.Tasks.Rest.v1._0.Requests.Task
{
    public record CreateTaskRequest(string Title, string? Description, Aggregates.TaskAggregate.TaskStatus Status, DateTime DueDate);
}
