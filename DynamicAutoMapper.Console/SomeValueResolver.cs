using AutoMapper;
using Source;
using Target;

namespace DynamicAutoMapper.Console
{
    public class SomeValueResolver(IService service) : IValueResolver<DataDto, Data, string?>
    {
        public string? Resolve(DataDto source, Data destination, string? destMember, ResolutionContext context)
        {
            return service.GetValue();
        }
    }

    public class SomeMemberValueResolver(IService service) : IMemberValueResolver<DataDto, Data, string?, string?>
    {
        public string? Resolve(DataDto source, Data destination, string? sourceMember, string? destMember, ResolutionContext context)
        {
            return service.GetValue();
        }
    }
}
