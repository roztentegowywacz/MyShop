using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Identity.Commands.SignIn;
using MyShop.Services.Identity.Commands.SignUp;

namespace MyShop.Api.Controllers
{
    [Route("")]
    public class IdentityController : ApiController
    {
        public IdentityController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<JsonWebToken>> SignIn(SignInCommand command)
        {
            var jwt = await _dispatcher.SendAndResponseDataAsync(command);

            return Ok(jwt);
        }

        [HttpGet("me")]
        public IActionResult Get()
        {
            return Content($"Your id: '{UserId}'");
        }
    }
}