using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Authentication.Commands.RefreshAccessToken;
using MyShop.Services.Authentication.Commands.RevokeAccessToken;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [Route("")]
    public class TokensController : ApiController
    {
        public TokensController(IDispatcher dispatcher) : base(dispatcher) { }

        [AllowAnonymous]
        [HttpPost("access-tokens/refresh")]
        public async Task<ActionResult<JsonWebToken>> RefreshAccessToken(RefreshAccessTokenCommand command)
        {
            var jwt = await _dispatcher.SendAndResponseDataAsync(command);

            return Ok(jwt);
        }

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken(RevokeAccessTokenCommand command)
        {
            await _dispatcher.SendAsync(command);

            return NoContent();
        }
    }
}