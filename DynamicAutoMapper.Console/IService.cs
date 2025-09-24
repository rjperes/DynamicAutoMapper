namespace DynamicAutoMapper.Console
{
    public interface IService
    {
        string? GetValue();
    }

    public class SomeService : IService
    {
        public string? GetValue()
        {
            return "SomeValue";
        }
    }
}
