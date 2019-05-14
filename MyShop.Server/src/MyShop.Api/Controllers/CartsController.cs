using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Carts.Commands;
using MyShop.Services.Carts.Dtos;
using MyShop.Services.Carts.Queries;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [JwtAuth]
    public class CartsController : ApiController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpPost("items/fill-cart")]
        public async Task<IActionResult> Post(FillCart command)
        {
            await _dispatcher.SendAsync(command);

            return CreatedAtAction(nameof(Get), new GetCart() { Id = command.CustomerId }, null);            
        }

        [HttpPost("items")]
        public async Task<IActionResult> Post(AddProductToCart command)
        {
            await _dispatcher.SendAsync(command.Bind(c => c.CustomerId, UserId));

            return CreatedAtAction(nameof(Get), new GetCart() { Id = command.CustomerId }, null);
        }

        [HttpGet("my")]
        public async Task<ActionResult<CartDto>> Get()
        {
            var cart = await _dispatcher.QueryAsync(new GetCart() { Id = UserId });

            return Single(cart);
        }

        [HttpDelete("items/{productId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            await _dispatcher.SendAsync(new DeleteProductFromCart(UserId, productId));

            return NoContent();
        }

        [HttpPost("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _dispatcher.SendAsync(new ClearCart(UserId));

            return NoContent();
        }
    }
}