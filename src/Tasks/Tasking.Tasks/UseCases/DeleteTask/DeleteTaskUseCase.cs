using Tasking.Tasks.Aggregates.TaskAggregate;

namespace Tasking.Tasks.UseCases.DeleteTask
{
    internal sealed class DeleteTaskUseCase : IDeleteTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task ExecuteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);

            if (task is null)
                throw Errors.TaskUseCasesErrors.TaskNotFoundForDelete;

            await _taskRepository.DeleteAsync(task, cancellationToken);
        }

        public void Dispose()
        {
            _taskRepository.Dispose();
        }
    }
}
