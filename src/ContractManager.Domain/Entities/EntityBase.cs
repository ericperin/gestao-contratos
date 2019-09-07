using System;

namespace ContractManager.Domain.Entities
{

    public interface IEntityBase
    {
        Guid Id { get; set; }
        DateTime? DeletedAt { get; set; }
        DateTime CreatedAt { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}