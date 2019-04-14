using MyShop.Services.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyShop.Services.Carts.Commands;
using MyShop.Services.Carts.Queries;
using MyShop.Services.Carts.Dtos;

namespace MyShop.Api.Controllers
{
    public class CartsController : ApiController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("items")]
        public async Task<IActionResult> Post(AddProductToCart command)
        {
            await _dispatcher.SendAsync(command);

            return CreatedAtAction(nameof(Get), new GetCart(){ Id = command.CustomerId }, null);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CartDto>> Get([FromRoute] GetCart query)
        {
            var cart = await _dispatcher.QueryAsync(query);
            
            return Single(cart);
        }
    }
}