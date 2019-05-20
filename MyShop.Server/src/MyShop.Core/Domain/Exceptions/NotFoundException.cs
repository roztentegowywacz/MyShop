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
    }
}