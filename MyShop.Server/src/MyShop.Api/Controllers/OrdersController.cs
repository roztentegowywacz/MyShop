using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Infrastructure.Mvc;
using MyShop.Infrastructure.Authentication;
using MyShop.Services.Orders.Dtos;
using System;
using Microsoft.AspNetCore.Authorization;
using MyShop.Services.Orders.Commands.CreateOrder;
using MyShop.Services.Orders.Queries.GetOrder;
using MyShop.Services.Orders.Queries.BrowseOrders;
using MyShop.Services.Orders.Commands.ChangeOrderStatus;
using MyShop.Services.Orders.Commands.CancelOrder;

namespace MyShop.Api.Controllers
{
    public class OrdersController : ApiController
    {
        public OrdersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        // TODO: Dodać logikę do opłacania zamówień.

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return CreatedAtAction(nameof(Get), new GetOrderQuery() { Id = command.Id }, null);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDetailsDto>> Get([FromRoute] GetOrderQuery query)
        {
            var order = await _dispatcher.QueryAsync(query);

            return Single(order);
        }

        [AdminAuth]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseOrdersQuery query)
        {
            var orders = await _dispatcher.QueryAsync(query);

            return Collection(orders);
        }

        [AdminAuth]
        [HttpPost("status")]
        public async Task<IActionResult> Put(ChangeOrderStatusCommand command)
        {
            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("cancel/{id:Guid}")]
        public async Task<IActionResult> Put(Guid id)
        {
            await _dispatcher.SendAsync(new CancelOrderCommand(id, UserId));

            return NoContent();
        }
    }
}