using MyShop.Services.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyShop.Services.Carts.Commands;
using MyShop.Services.Carts.Queries;
using MyShop.Services.Carts.Dtos;
using System;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Api.Controllers
{
    public class CartsController : ApiController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        // [JwtAuth]
        // [HttpPost("items")]
        // public async Task<IActionResult> Post(AddProductToCart command)
        // {
        //     await _dispatcher.SendAsync(command.Bind(c => c.CustomerId, UserId));

        //     return CreatedAtAction(nameof(Get), new GetCart(){ Id = command.CustomerId }, null);
        // }

        // [JwtAuth]
        // [HttpGet("cart")]
        // public async Task<ActionResult<CartDto>> Get()
        // {
        //     var cart = await _dispatcher.QueryAsync(new GetCart() { Id = UserId });
            
        //     return Single(cart);
        // }

        // [JwtAuth]
        // [HttpDelete("items/{productId:guid}")]
        // public async Task<IActionResult> Delete([FromRoute] Guid productId)
        // {
        //     await _dispatcher.SendAsync(new DeleteProductFromCart(UserId, productId));

        //     return NoContent();
        // }
    }
}