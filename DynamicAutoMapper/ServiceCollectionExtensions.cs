using Microsoft.Extensions.DependencyInjection;

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
                        var profile = ActivatorUtilities.CreateInstance<DynamicMappingProfile>(serviceProvider, options);
                        config.AddProfile(profile);
                    }
                }, options.AdditionalAssemblies ?? []);
        }
    }
}
