using System;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Types;
using MyShop.Infrastructure.Authentication;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [JwtAuth]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IDispatcher _dispatcher;

        public ApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected ActionResult<T> Single<T>(T data, Func<T, bool> criteria = null)
        {
            if (data == null)
            {
                return NotFound();
            }

            var isValid = criteria == null || criteria(data);
            if (isValid)
            {
                return Ok(data);
            }

            return NotFound();
        }

        protected ActionResult Collection<T>(PagedResults<T> pagedResults)
        {
            if (pagedResults is null)
            {
                return NotFound();
            }

            return Ok(pagedResults);
        }
    }
}