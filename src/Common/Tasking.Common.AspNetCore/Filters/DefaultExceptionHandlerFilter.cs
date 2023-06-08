using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Tasking.Shared;

namespace Tasking.Common.AspNetCore.Filters
{
    public class DefaultExceptionHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            const string NotFoundType = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";

            if (context.Exception is DomainException domainExcpetion)
            {
                var result = new ProblemDetails
                {
                    Detail = domainExcpetion.Message,
                    Status = StatusCodes.Status400BadRequest
                };

                result.Extensions.Add(new("Code", domainExcpetion.Code));

                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(result);
            }
            else if (context.Exception is EntityNotFoundException entityNotFoundExcpetion)
            {
                var result = new ProblemDetails
                {
                    Type = NotFoundType,
                    Title = entityNotFoundExcpetion.Message,
                    Status = StatusCodes.Status404NotFound
                };

                result.Extensions.Add(new("Code", entityNotFoundExcpetion.Code));

                context.ExceptionHandled = true;
                context.Result = new NotFoundObjectResult(result);
            }
            else if (context.Exception is ValidationException validationException)
            {
                var result = new ValidationProblemDetails();

                var errorsGroupedByProperty = validationException.Errors
                        .GroupBy(error => error.PropertyName);

                foreach (var propertyErrors in errorsGroupedByProperty)
                    result.Errors.Add(propertyErrors.Key, propertyErrors.Select(error => error.ErrorMessage).ToArray());

                context.ExceptionHandled = true;
                context.Result = new BadRequestObjectResult(result);
            }
            else
            {
                base.OnException(context);
            }
        }
    }
}
