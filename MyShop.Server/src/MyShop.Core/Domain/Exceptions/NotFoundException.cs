using System;

namespace MyShop.Core.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public ErrorCodes? Code { get; }
        public Guid? EntityId { get; }

        public NotFoundException()
        {
        }

        public NotFoundException(ErrorCodes code)
        {
            Code = code;
        }

        public NotFoundException(ErrorCodes code, Guid entityId)
            : this(code)
        {
            EntityId = entityId;
        }

        // public NotFoundException(string message, params object[] args)
        //     : this(null, message, args)
        // {
        // }

        // public NotFoundException(ErrorCodes code, string message, params object[] args) 
        //     : this(null, code, message, args)
        // {
        // }

        // public NotFoundException(Exception innerException, string message, params object[] args)
        //     : this(innerException, null, message, args)
        // {
        // }

        // public NotFoundException(Exception innerException, ErrorCodes? code, string message, params object[] args)
        //     : base(string.Format(message, args), innerException)
        // {
        //     Code = code;
        // }
    }
}