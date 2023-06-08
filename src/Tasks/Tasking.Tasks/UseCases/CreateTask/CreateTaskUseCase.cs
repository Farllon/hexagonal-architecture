using Tasking.Tasks.Aggregates.TaskAggregate;

namespace Tasking.Tasks.UseCases.CreateTask
{
    internal sealed class CreateTaskUseCase : ICreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<CreateTaskOutput> ExecuteAsync(CreateTaskInput input, CancellationToken cancellationToken)
        {
            var task = Aggregates.TaskAggregate.Task.Create(
                input.Title,
                input.Description,
                input.Status,
                input.DueDate);

            await _taskRepository.CreateAsync(task, cancellationToken);

            return new CreateTaskOutput(
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
