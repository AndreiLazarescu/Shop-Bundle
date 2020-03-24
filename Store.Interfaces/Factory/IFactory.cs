namespace Store.Interfaces.Factory
{
    public interface IFactory<T>
        where T : class
    {
        T GetInstance();
    }
}
