namespace MyShop.Core.Types
{
    public interface IPagedFilterQuery<T> : IPagedQuery, IFilterQuery<T>
    {
    }
}