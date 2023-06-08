using Microsoft.Extensions.DependencyInjection;

using Tasking.Tasks.Queries.GetTasks;
using Tasking.Tasks.InMemoryDb.Queries;
using Tasking.Tasks.Queries.GetTaskById;
using Tasking.Tasks.InMemoryDb.Repositories;
using Tasking.Tasks.Aggregates.TaskAggregate;

namespace Tasking.Tasks.InMemoryDb
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInMemoryDbAdapter(this IServiceCollection services)
        {
            services.AddSingleton<MemoryDb>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IGetTaskByIdQuery, GetTaskByIdQuery>();
            services.AddScoped<IGetTasksQuery, GetTasksQuery>();

            return services;
        }
    }
}
