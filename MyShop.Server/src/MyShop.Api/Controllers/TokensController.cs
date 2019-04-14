using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Domain.Authentication;
using MyShop.Infrastructure.Authentication;
using MyShop.Services.Authentication.Commands;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [Route("")]
    public class TokensController : ApiController
    {
        public TokensController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("access-tokens/refresh")]
        public async Task<ActionResult<JsonWebToken>> RefreshAccessToken(RefreshAccessToken command)
        {
            var jwt = await _dispatcher.SendAndResponseDataAsync(command);
            return Ok(jwt);
        }

        [JwtAuth]
        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken(RevokeAccessToken command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }
    }
}