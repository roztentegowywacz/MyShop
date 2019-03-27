using MyShop.Core.Domain.Authentication;

namespace MyShop.Services.Identity.Commands
{
    public class SignIn : ICommand<JsonWebToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}