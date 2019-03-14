namespace MyShop.Core.Types
{
    public abstract class PagedQueryBase : IPagedQuery
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }   
        public object ValueFrom { get; set; }
        public object ValueTo { get; set; }
    }
}