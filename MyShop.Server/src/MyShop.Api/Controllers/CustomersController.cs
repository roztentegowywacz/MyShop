using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Authentication.Services;
using MyShop.Services.Customers.Commands;
using MyShop.Services.Customers.Dtos;
using MyShop.Services.Customers.Queries;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    public class CustomersController : ApiController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            var user = User;
            await _dispatcher.SendAsync(command.Bind(c => c.Id, UserId));

            return CreatedAtAction(nameof(Get), new GetCustomer() { Id = command.Id }, null);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
        {
            var customer = await _dispatcher.QueryAsync(query);

            return Single(customer);
        }

        // TODO: dodać logikę, do uzupełniania konta dla klienta jeśli jest już zarejestrowanym użytkownikiem.
        // update?
    }
}