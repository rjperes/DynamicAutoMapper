using AutoMapper;

namespace DynamicAutoMapper.Console
{
    public class DataProfile : Profile
    {
        public DataProfile(IService service)
        {
            CreateMap<Source.DataDto, Target.Data>()
                .ForMember(target => target.Id, config => config.AddTransform(value => value * 10))
                ////.AfterMap((source, target) => target.SomeValue = service.GetValue())
                .ForMember(target => target.SomeValue, config => config.MapFrom<SomeValueResolver>())
                .ForMember(target => target.Name, config => config.MapFrom<SomeMemberValueResolver, string?>(source => source.Name))
                .ReverseMap();
        }
    }
}
