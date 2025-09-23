using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicAutoMapper.Console
{
    internal class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddOptions();
            services.AddAutoMapper(static options =>
            {
                options.SourceAssembly = typeof(Source.DataDto).Assembly;
                options.TargetAssembly = typeof(Target.Data).Assembly;
                options.TypeFilter = (source, target) => source.Name.StartsWith(target.Name);
                options.AdditionalAssemblies.Add(typeof(DataProfile).Assembly);
            });

            services.AddSingleton<IService, SomeService>();

            var serviceProvider = services.BuildServiceProvider();

            var mapper = serviceProvider.GetRequiredService<AutoMapper.IMapper>();

            foreach (var typeMap in mapper.ConfigurationProvider.Internal().GetAllTypeMaps())
            {
                System.Console.WriteLine($"{typeMap.SourceType.Name} -> {typeMap.DestinationType.Name}");
            }

            var source = new Source.DataDto { Id = 1, Name = "Test" };

            var target = mapper.Map<Target.Data>(source);

            target.Name += " - Mapped";

            source = mapper.Map<Source.DataDto>(target);
        }
    }
}
