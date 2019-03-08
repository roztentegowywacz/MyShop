using System;

namespace MyShop.Core.Domain
{
    public class BaseEntity : IIdentifiable
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            SetUpdatedDate();
        }

        protected void SetUpdatedDate()
            => UpdatedDate = DateTime.UtcNow;
    }
}