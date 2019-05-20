using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Customers.Commands.CompleteCustomer;
using MyShop.Services.Customers.Commands.CreateCustomer;
using MyShop.Services.Customers.Dtos;
using MyShop.Services.Customers.Queries.GetCustomer;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    public class CustomersController : ApiController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            var user = User;
            await _dispatcher.SendAsync(command.Bind(c => c.Id, UserId));

            return CreatedAtAction(nameof(Get), new GetCustomerQuery() { Id = command.Id }, null);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomerQuery query)
        {
            var customer = await _dispatcher.QueryAsync(query);

            return Single(customer);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdateCustomerCommand command)
        {
            await _dispatcher.SendAsync(command.Bind(c => c.Id, id));

            return NoContent();
        }        
    }
}