using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Orders.Commands;
using MyShop.Infrastructure.Mvc;
using MyShop.Infrastructure.Authentication;
using MyShop.Services.Orders.Dtos;
using MyShop.Services.Orders.Queries;

namespace MyShop.Api.Controllers
{
    [JwtAuth]
    public class OrdersController : ApiController
    {
        public OrdersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrder command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return CreatedAtAction(nameof(Get), new GetOrder() { Id = command.Id }, null);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDetailsDto>> Get([FromRoute] GetOrder query)
        {
            var order = await _dispatcher.QueryAsync(query);

            return Single(order);
        }

        [AdminAuth]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseOrders query)
        {
            var orders = await _dispatcher.QueryAsync(query);

            return Collection(orders);
        }
    }
}