using Tasking.Shared;
using Tasking.Tasks.Queries.GetTasks;

namespace Tasking.Tasks
{
    public static class Errors
    {
        public static class TaskAggregateErrors
        {
            public readonly static DomainException ShortTitle = new("001", $"Title must contain at least {Aggregates.TaskAggregate.Title.MinLength} characters");

            public readonly static DomainException LongTitle = new("002", $"The title must contain a maximum of {Aggregates.TaskAggregate.Title.MaxLength} characters");

            public readonly static DomainException ShortDescription = new("003", $"Description must contain at least {Aggregates.TaskAggregate.Description.MinLength} characters");

            public readonly static DomainException LongDescription = new("004", $"The description must contain a maximum of {Aggregates.TaskAggregate.Description.MaxLength} characters");

            public readonly static DomainException DueDateLessThanCurrent = new("005", $"The due date must be greater than or equal to the current date");

            public readonly static DomainException OverdueStatusManually = new("006", "Overdue status cannot be entered manually");

            public readonly static DomainException ChangeDateInvalidNewStatus = new("007", "The status when changing the task deadline must be Todo or Doing");
        }

        public static class TaskUseCasesErrors
        {
            public readonly static EntityNotFoundException TaskNotFoundForUpdate = new("008", "The task was not found");

            public readonly static EntityNotFoundException TaskNotFoundForDelete = new("009", "The task was not found");

            public readonly static DomainException TaskNotChange = new("010", "There was no change in task information");
        }

        public static class TaskQueriesErrors
        {
            public readonly static EntityNotFoundException TaskNotFound = new("011", "The task was not found");

            public readonly static DomainException InvalidPageSize = new("012", $"The page size must be between {IGetTasksQuery.MinSize} and {IGetTasksQuery.MaxSize}");

            public readonly static DomainException InvalidPage = new("013", $"The page number must be greather than {IGetTasksQuery.MinPage}");
        }
    }
}
