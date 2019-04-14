using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Customers.Commands;
using MyShop.Services.Customers.Dtos;
using MyShop.Services.Customers.Queries;
using MyShop.Services.Dispatchers;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Api.Controllers
{
    public class CustomersController : ApiController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            await _dispatcher.SendAsync(command.Bind(c => c.Id, UserId));

            return CreatedAtAction(nameof(Get), new GetCustomer(){ Id = command.Id }, null);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
        {
            var customer = await _dispatcher.QueryAsync(query);
            
            return Single(customer);
        }
    }
}