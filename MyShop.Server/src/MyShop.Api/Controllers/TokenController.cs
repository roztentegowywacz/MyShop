using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Authentication.Commands;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [Route("access-token")]
    public class TokenController : ApiController
    {
        public TokenController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("/refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<JsonWebToken>> RefreshAccessToken(RefreshAccessToken command)
        {
            var jwt = await _dispatcher.SendAndResponseDataAsync(command);
            return Ok(jwt);
        }
    }
}