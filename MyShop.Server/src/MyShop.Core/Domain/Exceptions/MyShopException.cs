using System;

namespace MyShop.Core.Domain.Exceptions
{
    public class MyShopException : Exception
    {
        public string Code { get; }

        public MyShopException()
        {
        }

        public MyShopException(string code)
        {
            Code = code;
        }

        public MyShopException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public MyShopException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public MyShopException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public MyShopException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}