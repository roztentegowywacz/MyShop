using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Types
{
    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Items { get; }

        protected PagedResult()
        {
            Items = Enumerable.Empty<T>();
        }

        protected PagedResult(IEnumerable<T> items, int currentPage,
            int resultsPerPage, int totalPages, int totalResults) :
                base(currentPage, resultsPerPage, totalPages, totalResults)
        {
            Items = items;
        }

        public static PagedResult<T> Empty => new PagedResult<T>();

        public static PagedResult<T> Create(IEnumerable<T> items, int currentPage,
            int resultsPerPage, int totalPages, int totalResults)
            => new PagedResult<T>(items, currentPage, resultsPerPage, totalPages, totalResults);
    }
}            