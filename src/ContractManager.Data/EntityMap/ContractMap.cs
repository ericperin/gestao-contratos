using ContractManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractManager.Data.EntityMap
{
    public class ContractMap : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.QuantityTraded).IsRequired();
            builder.Property(x => x.NegotiatedValue).IsRequired();
            builder.Property(x => x.StartedAt).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
            builder.Property(x => x.File).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeletedAt);
            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}
