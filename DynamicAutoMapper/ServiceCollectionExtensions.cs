using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DynamicAutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoAutoMapper(this IServiceCollection services, Action<AutoMappingOptions>? configureOptions = null)
        {
            ArgumentNullException.ThrowIfNull(services);

            AutoMappingOptions? options = default;

            if (configureOptions != null)
            {
                options ??= new AutoMappingOptions();
                configureOptions(options);
                services.Configure(configureOptions);
            }

            return services.AddAutoMapper((serviceProvider, config) =>
            {
                var profileTypes = (options?.AdditionalAssemblies ?? [])
                    .SelectMany(assembly => GetProfileTypes(assembly))
                    .ToList();

                foreach (var profileType in profileTypes)
                {
                    var profile = (Profile)ActivatorUtilities.CreateInstance(serviceProvider, profileType);
                    config.AddProfile(profile);
                }

                config.AddProfile(ActivatorUtilities.CreateInstance<AutoMappingProfile>(serviceProvider));
            }, Array.Empty<Assembly>());
        }

        private static IEnumerable<Type> GetProfileTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass && t.IsPublic);
        }
    }
}
