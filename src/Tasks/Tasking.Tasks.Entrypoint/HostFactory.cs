using Tasking.Tasks.Rest;
using Tasking.Tasks.InMemoryDb;
using Tasking.Common.AspNetCore.Filters;

namespace Tasking.Tasks.Entrypoint
{
    public static class HostFactory
    {
        public static IHost CreateApi(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddCustomRouting()
                .AddJwt(builder.Configuration)
                .AddCustomVersioning()
                .AddCustomCors(builder.Environment, builder.Configuration)
                .AddCustomSwagger("Tasks API", "This is the Tasks API")
                .AddTasks()
                .AddInMemoryDbAdapter()
                .AddRestAdapter()
                .AddControllers(options =>
                    options.Filters.Add<DefaultExceptionHandlerFilter>())
                .AddRestControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
                app.UseCustomSwagger();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

            return app;
        }
    }
}
