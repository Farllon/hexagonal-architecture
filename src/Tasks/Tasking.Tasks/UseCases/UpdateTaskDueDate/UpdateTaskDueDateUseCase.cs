using Tasking.Tasks.Aggregates.TaskAggregate;

namespace Tasking.Tasks.UseCases.UpdateTaskDueDate
{
    internal sealed class UpdateTaskDueDateUseCase : IUpdateTaskDueDateUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskDueDateUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<UpdateTaskDueDateOutput> ExecuteAsync(UpdateTaskDueDateInput input, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(input.TaskId, cancellationToken);

            if (task is null)
                throw Errors.TaskUseCasesErrors.TaskNotFoundForUpdate;

            if (task.DueDate.Value == input.NewDueDate)
                throw Errors.TaskUseCasesErrors.TaskNotChange;

            var newDueDate = DueDate.Create(input.NewDueDate);

            task.ChangeDueDate(newDueDate, input.NewStatus);

            await _taskRepository.UpdateAsync(task, cancellationToken);

            return new UpdateTaskDueDateOutput(
                task.Id,
                task.Title.Value,
                task.Description?.Value,
                task.Status,
                task.DueDate.Value);
        }

        public void Dispose()
        {
            _taskRepository.Dispose();
        }
    }
}
