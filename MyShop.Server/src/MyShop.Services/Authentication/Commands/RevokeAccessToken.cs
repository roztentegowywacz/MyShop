using System;
using Newtonsoft.Json;

namespace MyShop.Services.Authentication.Commands
{
    public class RevokeAccessToken : ICommand
    {
        public string RefreshToken { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public RevokeAccessToken(string refreshToken, Guid userId)
        {
            RefreshToken = refreshToken;
            UserId = userId;
        }
    }
}