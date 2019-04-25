using Microsoft.AspNetCore.Mvc;

namespace MyShop.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("MyShop Api works!");
    }
}