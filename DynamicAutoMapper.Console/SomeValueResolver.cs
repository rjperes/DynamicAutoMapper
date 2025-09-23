using AutoMapper;
using Source;
using Target;

namespace DynamicAutoMapper.Console
{
    public class SomeValueResolver : IValueResolver<DataDto, Data, string?>
    {
        public SomeValueResolver(IService service)
        {

        }

        public string? Resolve(DataDto source, Data destination, string? destMember, ResolutionContext context)
        {
            return "foobar";
        }
    }
}
