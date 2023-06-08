using FluentValidation;

using Tasking.Tasks.Rest.v1._0.Requests.Task;

namespace Tasking.Tasks.Rest.v1._0.Validators
{
    internal class UpdateTaskDueDateRequestValidator : AbstractValidator<UpdateTaskDueDateRequest>
    {
        public UpdateTaskDueDateRequestValidator()
        {
            RuleFor(request => request.NewStatus)
                .IsInEnum()
                .WithMessage("The status value is not valid");

            RuleFor(request => request.NewDueDate)
                .NotEmpty()
                .WithMessage("The due date must be informed");
        }
    }
}
