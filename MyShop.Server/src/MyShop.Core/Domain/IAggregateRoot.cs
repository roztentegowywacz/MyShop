using System.Collections.Generic;

namespace MyShop.Core.Domain
{
    public interface IAggregateRoot : IIdentifiable
    {
         IEnumerable<IEvent> Events { get; }
    }
}