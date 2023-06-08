using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasking.Shared;
using Tasking.Tasks.Aggregates.TaskAggregate;

namespace Tasking.Tasks.UseCases.UpdateTask
{
    internal sealed class UpdateTaskUseCase : IUpdateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<UpdateTaskOutput> ExecuteAsync(UpdateTaskInput input, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(input.TaskId, cancellationToken);

            if (task is null)
                throw Errors.TaskUseCasesErrors.TaskNotFoundForUpdate;

            var hasChanges = false;

            if (task.Title.Value != input.Title)
            {
                var newTitle = Title.Create(input.Title);

                task.Title = newTitle;

                hasChanges = true;
            }

            if (task.Description?.Value != input.Description &&
                input.Description is not null)
            {
                var newDescription = Description.Create(input.Description);

                task.Description = newDescription;

                hasChanges = true;
            }
            else if (task.Description?.Value != input.Description &&
                input.Description is null)
            {
                task.Description = null;

                hasChanges = true;
            }

            if (task.Status != input.Status)
            {
                task.ChangeStatus(input.Status);

                hasChanges = true;
            }

            if (!hasChanges)
                throw Errors.TaskUseCasesErrors.TaskNotChange;

            await _taskRepository.UpdateAsync(task, cancellationToken);

            return new UpdateTaskOutput(
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
