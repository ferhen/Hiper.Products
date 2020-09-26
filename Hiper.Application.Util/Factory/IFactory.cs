namespace Hiper.Application.Util.Factory
{
    public interface IFactory<T>
    {
        T Create();
    }
}
