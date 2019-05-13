using System;
using System.Text.RegularExpressions;
using MyShop.Core.Domain.Exceptions;

namespace MyShop.Core.Domain.Customers
{
    public class Customer : BaseEntity
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant
        );
        
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public bool Completed => CompletedAt.HasValue;
        public DateTime? CompletedAt { get; private set; }

        public Customer(Guid id, string email) : base(id)
        {
            SetEmail(email);
        }

        public void Complete(string firstName, string lastName, string address)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetAddress(address);
            CompletedAt = DateTime.UtcNow;
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

        // TODO: Add validation.
        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
            SetUpdatedDate();
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
            SetUpdatedDate();
        }

        public void SetAddress(string address)
        {
            Address = address;
            SetUpdatedDate();
        }
    }
}