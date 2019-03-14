namespace MyShop.Core.Types
{
    public interface IPagedQuery
    {
        int Page { get; }
        int ResultsPerPage { get; }
        string OrderBy { get; }
        string SortOrder { get; }
        object ValueFrom { get; }
        object ValueTo { get; }
    }
}