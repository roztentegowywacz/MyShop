using System;

namespace MyShop.Core.Domain
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}