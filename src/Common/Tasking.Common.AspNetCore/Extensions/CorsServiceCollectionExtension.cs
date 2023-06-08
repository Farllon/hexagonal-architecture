using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CorsServiceCollectionExtension
    {
        public const string PolicyName = "CustomPolicy";

        public static IServiceCollection AddCustomCors(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
        {
            var corsOrigins = configuration.GetSection("CorsOrigins").Get<List<string>>() ?? new List<string>();

            services.AddCors(op =>
            {
                op.AddPolicy(PolicyName, builder =>
                {
                    if (corsOrigins.Count == 0)
                        builder.AllowAnyOrigin();
                    else
                        builder.WithOrigins(corsOrigins.ToArray());

                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
