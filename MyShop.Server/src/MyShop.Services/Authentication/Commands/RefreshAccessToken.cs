using MyShop.Core.Domain.Authentication;
using Newtonsoft.Json;

namespace MyShop.Services.Authentication.Commands
{
    public class RefreshAccessToken : ICommand<JsonWebToken>
    {
        public string Token { get; }

        [JsonConstructor]
        public RefreshAccessToken(string token)
        {
            Token = token;
        }
    }
}