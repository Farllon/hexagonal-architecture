using System.Reflection;

using FluentValidation.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ValidationServiceCollectionExtension
    {
        public static IServiceCollection AddValidations(this IServiceCollection services, params Assembly[] assemblies)
        {
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(assemblies));
#pragma warning restore CS0618 // O tipo ou membro é obsoleto

            return services;
        }
    }
}
