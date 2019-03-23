using System;

namespace MyShop.Core.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Code { get; }

        public NotFoundException()
        {
        }

        public NotFoundException(string code)
        {
            Code = code;
        }

        public NotFoundException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public NotFoundException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public NotFoundException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public NotFoundException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}