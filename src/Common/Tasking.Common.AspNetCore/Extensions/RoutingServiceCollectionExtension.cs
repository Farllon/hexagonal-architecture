using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RoutingServiceCollectionExtension
    {
        public static IServiceCollection AddCustomRouting(this IServiceCollection services, Action<RouteOptions>? configureOptions = null)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;

                configureOptions?.Invoke(options);
            });

            return services;
        }
    }
}
