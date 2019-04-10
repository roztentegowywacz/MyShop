namespace MyShop.Services
{
    // Marker Interface
    public interface IQuery
    {
    }

    public interface IQuery<TResult> : IQuery
    {
    }
}