using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Domain.Authentication;
using MyShop.Services.Authentication.Commands;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [Route("")]
    public class TokenController : ApiController
    {
        public TokenController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [Route("access-token/refresh")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<JsonWebToken>> RefreshAccessToken(RefreshAccessToken command)
        {
            var jwt = await _dispatcher.SendAndResponseDataAsync(command);
            return Ok(jwt);
        }
    }
}