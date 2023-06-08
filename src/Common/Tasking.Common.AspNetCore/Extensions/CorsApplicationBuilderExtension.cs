using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class CorsApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(CorsServiceCollectionExtension.PolicyName);

            return app;
        }
    }
}
