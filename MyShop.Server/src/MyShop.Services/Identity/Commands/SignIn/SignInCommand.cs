using MyShop.Core.Domain.Authentication;
using Newtonsoft.Json;

namespace MyShop.Services.Identity.Commands.SignIn
{
    public class SignInCommand : ICommand<JsonWebToken> 
    {
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignInCommand (string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}