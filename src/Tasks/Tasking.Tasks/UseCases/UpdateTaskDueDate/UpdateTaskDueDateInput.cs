namespace Tasking.Tasks.UseCases.UpdateTaskDueDate
{
    public record UpdateTaskDueDateInput(Guid TaskId, DateTime NewDueDate, Aggregates.TaskAggregate.TaskStatus NewStatus);
}
