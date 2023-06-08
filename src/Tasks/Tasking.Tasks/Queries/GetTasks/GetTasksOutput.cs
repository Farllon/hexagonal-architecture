namespace Tasking.Tasks.Queries.GetTasks
{
    public record GetTasksOutput(IReadOnlyCollection<TaskDTO> Records, int Page, int TotalCount, int TotalPages);
}
