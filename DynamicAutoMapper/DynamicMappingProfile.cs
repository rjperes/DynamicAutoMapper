using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DynamicAutoMapper
{
    public class DynamicMappingOptions
    {
        public Assembly? SourceAssembly { get; set; }
        public Assembly? TargetAssembly { get; set; }
        public List<Assembly> AdditionalAssemblies { get; set; } = [];
        public bool Reverse { get; set; } = true;
        public Func<Type, Type, bool> TypeFilter { get; set; } = (source, target) => source.Name.StartsWith(target.Name);
    }

    public class DynamicMappingProfile : Profile
    {
        public DynamicMappingProfile(IOptions<DynamicMappingOptions> options, ILogger<DynamicMappingProfile> logger)
        {
            if (options?.Value.SourceAssembly != null && options?.Value.TargetAssembly != null)
            {
                var sourceTypes = GetTypes(options.Value.SourceAssembly);
                var targetTypes = GetTypes(options.Value.TargetAssembly);

                foreach (var sourceType in sourceTypes)
                {
                    var matchedTargetTypes = FindMatchingTypes(sourceType, targetTypes, options.Value.TypeFilter);

                    foreach (var matchedTargetType in matchedTargetTypes)
                    {
                        var expression = CreateMap(sourceType, matchedTargetType);

                        logger.LogInformation("Created mapping from {SourceType} to {TargetType}, reverse: {Reverse}", sourceType.FullName, matchedTargetType.FullName, options.Value.Reverse);

                        if (options.Value.Reverse)
                        {
                            expression.ReverseMap();
                        }
                    }
                }
            }
        }

        private static IEnumerable<Type> GetTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.IsClass && x.IsPublic && !x.IsAbstract && !x.IsInterface && !x.IsGenericType && !x.IsGenericTypeDefinition);
        }

        private static IEnumerable<Type> FindMatchingTypes(Type source, IEnumerable<Type> domainModelTypes, Func<Type, Type, bool> typeFilter)
        {
            return domainModelTypes.Where(target => typeFilter(source, target));
        }
    }
}
