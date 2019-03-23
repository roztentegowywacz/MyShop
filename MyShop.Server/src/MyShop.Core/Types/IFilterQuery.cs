namespace MyShop.Core.Types
{
    public interface IFilterQuery<T>
    {
        T ValueFrom { get; }
        T ValueTo { get; }
    }
}