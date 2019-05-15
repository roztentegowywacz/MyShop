using System;

namespace MyShop.Core.Domain.Exceptions
{
    public class MyShopException : Exception
    {
        public ErrorCodes? Code { get; }

        public MyShopException()
        {
        }

        public MyShopException(ErrorCodes code)
        {
            Code = code;
        }

        public MyShopException(string message, params object[] args)
            : this(null, message, args)
        {
        }

        public MyShopException(ErrorCodes code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public MyShopException(Exception innerException, string message, params object[] args)
            : this(innerException, null, message, args)
        {
        }

        public MyShopException(Exception innerException, ErrorCodes? code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}