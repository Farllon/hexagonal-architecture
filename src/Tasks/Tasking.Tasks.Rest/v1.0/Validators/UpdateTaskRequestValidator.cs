using FluentValidation;

using Tasking.Tasks.Rest.v1._0.Requests.Task;

namespace Tasking.Tasks.Rest.v1._0.Validators
{
    internal class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(request => request.Title)
                .MinimumLength(Aggregates.TaskAggregate.Title.MinLength)
                .WithMessage(Errors.TaskAggregateErrors.ShortTitle.Message)
                .WithErrorCode(Errors.TaskAggregateErrors.ShortTitle.Code)
                .MaximumLength(Aggregates.TaskAggregate.Title.MaxLength)
                .WithMessage(Errors.TaskAggregateErrors.LongTitle.Message)
                .WithErrorCode(Errors.TaskAggregateErrors.LongTitle.Code);

            RuleFor(request => request.Description)
                .MinimumLength(Aggregates.TaskAggregate.Description.MinLength)
                .WithMessage(Errors.TaskAggregateErrors.ShortDescription.Message)
                .WithErrorCode(Errors.TaskAggregateErrors.ShortDescription.Code)
                .MaximumLength(Aggregates.TaskAggregate.Description.MaxLength)
                .WithMessage(Errors.TaskAggregateErrors.LongDescription.Message)
                .WithErrorCode(Errors.TaskAggregateErrors.LongDescription.Code)
                .When(request => request.Description is not null);

            RuleFor(request => request.Status)
                .IsInEnum()
                .WithMessage("The status value is not valid");
        }
    }
}
