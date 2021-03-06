namespace MyShop.Core.Types
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; }
        public int ResultsPerPage { get; }
        public int TotalPages { get; }
        public int TotalResults { get; }

        protected PagedResultBase()
        {
        }

        protected PagedResultBase(int currentPage,
            int resultsPerPage, int totalPages, int totalResults)
        {
            CurrentPage = currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;    
        }
    }
}