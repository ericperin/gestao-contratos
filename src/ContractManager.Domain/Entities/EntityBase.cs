using System;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Removido em")]
        public DateTime? DeletedAt { get; set; }

        [Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }
    }
}