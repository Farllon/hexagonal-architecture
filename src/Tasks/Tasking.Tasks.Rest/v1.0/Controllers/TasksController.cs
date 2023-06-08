using System.Net.Mime;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Tasking.Tasks.UseCases.CreateTask;
using Tasking.Tasks.UseCases.DeleteTask;
using Tasking.Tasks.UseCases.UpdateTask;
using Tasking.Tasks.Rest.v1._0.Requests.Task;
using Tasking.Tasks.UseCases.UpdateTaskDueDate;
using Tasking.Tasks.Queries.GetTaskById;
using Tasking.Tasks.Queries.GetTasks;

namespace Tasking.Tasks.Rest.v1._0.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TasksController : ControllerBase, IDisposable
    {
        private readonly IGetTasksQuery _getTasksQuery;
        private readonly IGetTaskByIdQuery _getTaskByIdQuery;
        private readonly ICreateTaskUseCase _createTaskUseCase;
        private readonly IUpdateTaskUseCase _updateTaskUseCase;
        private readonly IDeleteTaskUseCase _deleteTaskUseCase;
        private readonly IUpdateTaskDueDateUseCase _updateTaskDueDateUseCase;

        public TasksController(
            IGetTasksQuery getTasksQuery,
            IGetTaskByIdQuery getTaskByIdQuery,
            ICreateTaskUseCase createTaskUseCase, 
            IUpdateTaskUseCase updateTaskUseCase, 
            IDeleteTaskUseCase deleteTaskUseCase, 
            IUpdateTaskDueDateUseCase updateTaskDueDateUseCase)
        {
            _getTasksQuery = getTasksQuery;
            _getTaskByIdQuery = getTaskByIdQuery;
            _createTaskUseCase = createTaskUseCase;
            _updateTaskUseCase = updateTaskUseCase;
            _deleteTaskUseCase = deleteTaskUseCase;
            _updateTaskDueDateUseCase = updateTaskDueDateUseCase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTasksOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(
            CancellationToken cancellationToken,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            var input = new GetTasksInput(page, size);

            var tasksPage = await _getTasksQuery.ExecuteAsync(input, cancellationToken);

            return Ok(tasksPage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateTaskOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateTaskRequest request,
            [FromServices] IValidator<CreateTaskRequest> validator,
            CancellationToken cancellationToken)
        {
            validator.ValidateAndThrow(request);

            var input = new CreateTaskInput(
                request.Title,
                request.Description,
                request.Status,
                request.DueDate);

            var createdTask = await _createTaskUseCase.ExecuteAsync(input, cancellationToken);

            return CreatedAtAction("GetById", new { id = createdTask.Id }, createdTask);
        }

        [HttpGet("{id:guid}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var task = await _getTaskByIdQuery.ExecuteAsync(id, cancellationToken);

            if (task is null)
                return NotFound();

            return Ok(task);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTaskOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateTaskRequest request,
            [FromServices] IValidator<UpdateTaskRequest> validator,
            CancellationToken cancellationToken)
        {
            validator.ValidateAndThrow(request);

            var input = new UpdateTaskInput(
                id,
                request.Title,
                request.Description,
                request.Status);

            var updatedTask = await _updateTaskUseCase.ExecuteAsync(input, cancellationToken);

            return Ok(updatedTask);
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTaskOutput))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTaskDueDateOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDueDateAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateTaskDueDateRequest request,
            [FromServices] IValidator<UpdateTaskDueDateRequest> validator,
            CancellationToken cancellationToken)
        {
            validator.ValidateAndThrow(request);

            var input = new UpdateTaskDueDateInput(
                id,
                request.NewDueDate,
                request.NewStatus);

            var updatedTask = await _updateTaskDueDateUseCase.ExecuteAsync(input, cancellationToken);

            return Ok(updatedTask);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await _deleteTaskUseCase.ExecuteAsync(id, cancellationToken);

            return NoContent();
        }

        public void Dispose()
        {
            _getTasksQuery.Dispose();
            _getTaskByIdQuery.Dispose();
            _createTaskUseCase.Dispose();
            _updateTaskUseCase.Dispose();
            _deleteTaskUseCase.Dispose();
            _updateTaskDueDateUseCase.Dispose();
        }
    }
}
