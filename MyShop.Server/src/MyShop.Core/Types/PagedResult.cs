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

        public static PagedResult<T> Empty => new PagedResult<T>();
    }
}            