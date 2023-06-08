using Tasking.Shared;

namespace Tasking.Tasks.Aggregates.TaskAggregate
{
    public class Task : Entity, IAggregateRoot
    {
        public Title Title { get; set; } = default!;

        public Description? Description { get; set; }

        private TaskStatus _status;

        public TaskStatus Status
        {
            get
            {
                return DueDate.Value < DateTime.UtcNow
                    ? TaskStatus.Overdue
                    : _status;
            }
            private set
            {
                _status = value;
            }
        }

        public DueDate DueDate { get; private set; } = default!;

        private Task(Title title, Description? description, TaskStatus status, DueDate dueDate)
        {
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
        }

        public static Task Create(string title, string? description, TaskStatus status, DateTime dueDate)
        {
            if (status is TaskStatus.Overdue)
                throw Errors.TaskAggregateErrors.OverdueStatusManually;

            var taskTitle = Title.Create(title);

            Description? taskDescription = null;

            if (description is not null)
                taskDescription = Description.Create(description);

            var taskDueDate = DueDate.Create(dueDate);

            var task = new Task(taskTitle, taskDescription, status, taskDueDate);

            return task;
        }

        public void ChangeStatus(TaskStatus status)
        {
            if (status is TaskStatus.Overdue)
                throw Errors.TaskAggregateErrors.OverdueStatusManually;

            Status = status;
        }

        public void ChangeDueDate(DueDate dueDate, TaskStatus newStatus)
        {
            if (newStatus is not TaskStatus.Todo ||
                newStatus is not TaskStatus.Doing)
                throw Errors.TaskAggregateErrors.ChangeDateInvalidNewStatus;

            DueDate = dueDate;
            Status = newStatus;
        }
    }
}
