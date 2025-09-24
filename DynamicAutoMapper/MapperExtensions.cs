using AutoMapper;
using AutoMapper.Internal;

namespace DynamicAutoMapper
{
    public static class MapperExtensions
    {
        public static IEnumerable<TypeMap> GetAllTypeMaps(this IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(mapper);

            return mapper.ConfigurationProvider.Internal().GetAllTypeMaps();
        }
    }
}
