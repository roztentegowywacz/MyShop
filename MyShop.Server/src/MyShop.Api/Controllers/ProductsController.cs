using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Products;
using MyShop.Services.Products.Dtos;
using MyShop.Services.Products.Queries;

namespace MyShop.Api.Controllers
{
    public class ProductsController : ApiController
    {
        public ProductsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
            var productDto = await _dispatcher.QueryAsync<ProductDto>(new GetProduct() { Id = id });
            
            return Single(productDto);
        }
    }
}
