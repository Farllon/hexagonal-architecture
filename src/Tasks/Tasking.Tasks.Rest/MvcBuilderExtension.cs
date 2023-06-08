using Microsoft.Extensions.DependencyInjection;

namespace Tasking.Tasks.Rest
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddRestControllers(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(MvcBuilderExtension).Assembly);

            return builder;
        }
    }
}
