using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceCollectionExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string apiTitle, string apiDescription)
        {
            ConfigureSwaggerOptions.ApiTitle = apiTitle;
            ConfigureSwaggerOptions.ApiDescription = apiDescription;

            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }

        private class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider _provider;

            public static string ApiTitle = "API";
            public static string ApiDescription = "This is an API";

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            {
                _provider = provider;
            }

            public void Configure(SwaggerGenOptions options)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token: Bearer {your token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                foreach (var description in _provider.ApiVersionDescriptions)
                    options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }

            public void Configure(string name, SwaggerGenOptions options)
            {
                Configure(options);
            }

            private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
            {
                var info = new OpenApiInfo
                {
                    Version = description.ApiVersion.ToString(),
                    Title = ApiTitle,
                    Description = ApiDescription,
                    Contact = new OpenApiContact
                    {
                        Name = "Farllon Augusto",
                        Email = "farllon.costa@outlook.com"
                    }
                };

                if (description.IsDeprecated)
                {
                    info.Description += " This API version has been deprecated.";
                }

                return info;
            }
        }
    }
}
