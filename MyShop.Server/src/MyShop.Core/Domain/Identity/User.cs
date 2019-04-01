using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using MyShop.Core.Domain.Exceptions;

namespace MyShop.Core.Domain.Identity
{
    public class User : BaseEntity
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

        public string Email { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }
     
     
        public User(Guid id, string email, string role = Identity.Role.User) : base(id)
        {
            SetEmail(email);
            SetRole(role);
        }


        public void SetEmail(string email)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new MyShopException("invalid_email_address",
                    $"Invalid email: '{email}'.");
            }

            Email = email.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        public void SetRole(string role)
        {
            if (!Identity.Role.IsValid(role))
            {
                throw new MyShopException("invalid_role",
                    $"Invalid role: '{role}'.");
            }

            Role = role.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new MyShopException("invalid_password",
                    "Password can not be empty.");
            }

            PasswordHash = passwordHasher.HashPassword(this, password);
        }
    }
}