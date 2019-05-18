using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Domain;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Products;
using MyShop.Services.Products.Commands;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries;

namespace MyShop.Api.Controllers
{
    public class ProductsController : ApiController
    {
        public ProductsController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [AdminAuth]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProduct command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return CreatedAtAction(nameof(Get), new GetProduct() { Id = command.Id }, null);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] GetProduct query)
        {
            var product = await _dispatcher.QueryAsync(query);

            return Single(product);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseProducts query)
        {
            var products = await _dispatcher.QueryAsync(query);

            return Collection(products);
        }

        [AdminAuth]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateProduct command)
        {
            await _dispatcher.SendAsync(command.Bind(c => c.Id, id));

            return NoContent();
        }

        [AdminAuth]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.SendAsync(new DeleteProduct(id));

            return NoContent();
        }
    }
}