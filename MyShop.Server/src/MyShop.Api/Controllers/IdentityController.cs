using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using MyShop.Infrastructure.Authentication;
using MyShop.Infrastructure.Mvc;
using MyShop.Services.Dispatchers;
using MyShop.Services.Identity.Commands;
using MyShop.Services.Identity.Handlers;

namespace MyShop.Api.Controllers
{
    [Route("")]
    public class IdentityController : ApiController
    {
        public IdentityController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<JsonWebToken>> SignIn(SignIn command)
        {
            var jwt = await _dispatcher.SendAndResponseDataAsync(command);

            return Ok(jwt);
        }

        [JwtAuth]
        [HttpGet("me")]
        public IActionResult Get()
        {
            return Content($"Your id: '{UserId}'");
        }
    }
}