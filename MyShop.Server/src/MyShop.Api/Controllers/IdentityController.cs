using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Identity.Commands;
using MyShop.Infrastructure.Mvc;

namespace MyShop.Api.Controllers
{
    public class IdentityController : ApiController
    {
        public IdentityController(IDispatcher dispatcher) : base(dispatcher)
        {
        }
        
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));    

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            var jwt = await _dispatcher.SendAsync(command);

            return Ok(jwt);
        }
    }
}