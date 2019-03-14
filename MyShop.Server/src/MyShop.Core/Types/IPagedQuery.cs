namespace MyShop.Core.Types
{
    public interface IPagedQuery
    {
        int Page { get; }
        int Results { get; }
        string OrderBy { get; }
        string SortOrder { get; }
    }
}