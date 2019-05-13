using System.Linq;
using System.Threading.Tasks;
using MyShop.Core.Domain.Orders.Repositories;
using MyShop.Core.Types;
using MyShop.Services.Orders.Dtos;
using MyShop.Services.Orders.Queries;

namespace MyShop.Services.Orders.Handlers
{
    public class BrowseOrdersHandler : IQueryHandler<BrowseOrders, PagedResults<OrderDto>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public BrowseOrdersHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<PagedResults<OrderDto>> HandleAsync(BrowseOrders query)
        {
            var pagedResult = await _ordersRepository.BrowseAsync(query);
            var orders = pagedResult.Items.Select(o => new OrderDto()
            {
                Id = o.Id,
                ItemsCount = o.Cart.Items.Count(),
                TotalAmount = o.TotalAmount,
                Status = o.Status
            }).ToList(); 

            return PagedResults<OrderDto>.From(pagedResult, orders);
        }
    }
}