using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Types
{
    public class PagedResults<T> : PagedResultBase
    {
        public IEnumerable<T> Items { get; }

        protected PagedResults()
        {
            Items = Enumerable.Empty<T>();
        }

        protected PagedResults(IEnumerable<T> items, int currentPage,
            int resultsPerPage, int totalPages, int totalResults) :
                base(currentPage, resultsPerPage, totalPages, totalResults)
        {
            Items = items;
        }

        public static PagedResults<T> Empty => new PagedResults<T>();

        public static PagedResults<T> Create(IEnumerable<T> items, int currentPage,
            int resultsPerPage, int totalPages, int totalResults)
            => new PagedResults<T>(items, currentPage, resultsPerPage, totalPages, totalResults);

        public static PagedResults<T> From(PagedResultBase result, IEnumerable<T> items)
            => Create(items, result.CurrentPage, result.ResultsPerPage, 
                result.TotalPages, result.TotalResults);
    }
}            