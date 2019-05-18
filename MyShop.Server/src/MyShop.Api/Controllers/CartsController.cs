using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Carts.Commands.AddProductToCart;
using MyShop.Services.Carts.Commands.ClearCart;
using MyShop.Services.Carts.Commands.DeleteProductFromCart;
using MyShop.Services.Carts.Commands.FillCart;
using MyShop.Services.Carts.Dtos;
using MyShop.Services.Carts.Queries.GetCart;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [JwtAuth]
    public class CartsController : ApiController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpPost("items/fill-cart")]
        public async Task<IActionResult> Post(FillCartCommand command)
        {
            await _dispatcher.SendAsync(command);

            return CreatedAtAction(nameof(Get), new GetCartQuery() { Id = command.CustomerId }, null);            
        }

        [HttpPost("items")]
        public async Task<IActionResult> Post(AddProductToCartCommand command)
        {
            await _dispatcher.SendAsync(command.Bind(c => c.CustomerId, UserId));

            return CreatedAtAction(nameof(Get), new GetCartQuery() { Id = command.CustomerId }, null);
        }

        [HttpGet("my")]
        public async Task<ActionResult<CartDto>> Get()
        {
            var cart = await _dispatcher.QueryAsync(new GetCartQuery() { Id = UserId });

            return Single(cart);
        }

        [HttpDelete("items/{productId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            await _dispatcher.SendAsync(new DeleteProductFromCartCommand(UserId, productId));

            return NoContent();
        }

        [HttpPost("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _dispatcher.SendAsync(new ClearCartCommand(UserId));

            return NoContent();
        }
    }
}