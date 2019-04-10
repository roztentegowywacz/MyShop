using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyShop.Core.Domain
{
    public class BaseEntity : IIdentifiable
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public BaseEntity(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            SetUpdatedDate();
        }

        protected void SetUpdatedDate()
            => UpdatedAt = DateTime.UtcNow;
    }
}