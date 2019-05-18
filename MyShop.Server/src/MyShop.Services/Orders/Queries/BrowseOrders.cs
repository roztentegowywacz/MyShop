using MyShop.Core.Domain.Orders;
using MyShop.Core.Types;
using MyShop.Services.Orders.Dtos;

namespace MyShop.Services.Orders.Queries
{
    public class BrowseOrders : IPagedFilterQuery<OrderStatus>, IQuery<PagedResults<OrderDto>>
    {
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 10;
        public string OrderBy { get; set; } = "UpdatedAt";
        public SortOrder SortOrder { get; set; } = SortOrder.asc;

        public OrderStatus Status { get; set; }


        OrderStatus IFilterQuery<OrderStatus>.ValueFrom
        {
            get { return this.Status; }
        }

        OrderStatus IFilterQuery<OrderStatus>.ValueTo
        {
            get { return  this.Status; }
        }
    }
}