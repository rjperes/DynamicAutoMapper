using AutoMapper;

namespace DynamicAutoMapper.Console
{
    public class DataProfile : Profile
    {
        public DataProfile(IService service)
        {
            CreateMap<Source.DataDto, Target.Data>()
                .ForMember(x => x.Id, target => target.AddTransform(source => source * 10))
                ////.AfterMap((s, t) => t.SomeValue = service.GetValue())
                .ForMember(t => t.SomeValue, x => x.MapFrom<SomeValueResolver>())
                .ForMember(t => t.Name, x => x.MapFrom<SomeMemberValueResolver, string?>(s => s.Name))
                .ReverseMap();
        }
    }
}
