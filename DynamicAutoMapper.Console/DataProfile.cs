using AutoMapper;

namespace DynamicAutoMapper.Console
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Source.DataDto, Target.Data>()
                .ForMember(x => x.Id, target => target.AddTransform(source => source * 10))
                .ForMember(x => x.SomeValue, x => x.MapFrom<SomeValueResolver>())
                .ReverseMap();
        }
    }
}
