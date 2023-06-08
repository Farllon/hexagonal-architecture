using Microsoft.Extensions.DependencyInjection;

using Tasking.Tasks.UseCases.CreateTask;
using Tasking.Tasks.UseCases.DeleteTask;
using Tasking.Tasks.UseCases.UpdateTask;
using Tasking.Tasks.UseCases.UpdateTaskDueDate;

namespace Tasking.Tasks
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTasks(this IServiceCollection services)
        {
            services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
            services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
            services.AddScoped<IUpdateTaskDueDateUseCase, UpdateTaskDueDateUseCase>();
            services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();

            return services;
        }
    }
}
