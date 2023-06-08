namespace Tasking.Tasks.Rest.v1._0.Requests.Task
{
    public record UpdateTaskDueDateRequest(DateTime NewDueDate, Aggregates.TaskAggregate.TaskStatus NewStatus);
}
