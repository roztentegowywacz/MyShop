using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace MyShop.Infrastructure.Authentication
{
    public class JwtValidatorMiddleware : IMiddleware
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtValidatorMiddleware(IDistributedCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await IsCurrentActiveToken())
            {
                await next(context);
                return;
            }
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }

        private async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        private async Task<bool> IsActiveAsync(string token)
            => string.IsNullOrWhiteSpace(await _cache.GetStringAsync($"tokens:{token}"));

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return string.IsNullOrWhiteSpace(authorizationHeader)
                ? string.Empty
                : authorizationHeader.Single().Split(' ').Last();
        }
    }
}