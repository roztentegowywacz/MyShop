using System;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Identity;

namespace MyShop.Core.Domain.Authentication
{
    public class RefreshToken : IIdentifiable
    {

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            Token = CreateToken(user, passwordHasher);
            CreatedAt = DateTime.UtcNow;
        }

        private string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"));
    }

}