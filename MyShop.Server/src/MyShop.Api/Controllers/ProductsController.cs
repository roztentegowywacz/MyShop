using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Products.Commands.CreateProduct;
using MyShop.Services.Products.Commands.DeleteProduct;
using MyShop.Services.Products.Commands.UpdateProduct;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries.BrowseProducts;
using MyShop.Services.Products.Queries.GetProduct;

namespace MyShop.Api.Controllers
{
    public class ProductsController : ApiController
    {
        public ProductsController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [AdminAuth]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return CreatedAtAction(nameof(Get), new GetProductQuery() { Id = command.Id }, null);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] GetProductQuery query)
        {
            var product = await _dispatcher.QueryAsync(query);

            return Single(product);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseProductsQuery query)
        {
            var products = await _dispatcher.QueryAsync(query);

            return Collection(products);
        }

        [AdminAuth]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            await _dispatcher.SendAsync(command.Bind(c => c.Id, id));

            return NoContent();
        }

        [AdminAuth]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _dispatcher.SendAsync(new DeleteProductCommand(id));

            return NoContent();
        }
    }
}