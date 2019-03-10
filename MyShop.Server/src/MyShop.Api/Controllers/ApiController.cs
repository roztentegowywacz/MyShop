using System;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Dispatchers;

namespace MyShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
    }
}