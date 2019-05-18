namespace MyShop.Core.Types
{
    public interface IPagedQuery
    {
        int Page { get; }
        int ResultsPerPage { get; }
        string OrderBy { get; }
        SortOrder SortOrder { get; }
    }

    public enum SortOrder : byte
    {
        asc = 0,
        desc = 1
    }
}