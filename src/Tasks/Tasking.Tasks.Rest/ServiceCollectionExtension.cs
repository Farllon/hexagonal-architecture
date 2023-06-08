using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Tasking.Tasks.Rest
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRestAdapter(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
