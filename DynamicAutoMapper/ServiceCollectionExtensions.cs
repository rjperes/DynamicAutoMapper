using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DynamicAutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<DynamicMappingOptions>? configureOptions = null)
        {
            DynamicMappingOptions options = new();

            return services.AddAutoMapper((serviceProvider, config) =>
                {
                    if (configureOptions != null)
                    {
                        configureOptions(options);

                        var profileTypes = (options.AdditionalAssemblies ?? [])
                            .SelectMany(assembly => GetProfileTypes(assembly))
                            .ToList();

                        foreach (var profileType in profileTypes)
                        {
                            var profile = (Profile)ActivatorUtilities.CreateInstance(serviceProvider, profileType);
                            config.AddProfile(profile);
                        }

                        config.AddProfile(new DynamicMappingProfile(options));
                    }
                }, options.AdditionalAssemblies ?? []);
        }

        private static IEnumerable<Type> GetProfileTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass && t.IsPublic);
        }
    }
}
