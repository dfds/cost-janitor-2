using CostJanitor.Domain.Aggregates;
using CostJanitor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostJanitor.Infrastructure.EntityFramework.Configurations
{
    public class CostItemEntityTypeConfiguration : IEntityTypeConfiguration<CostItem>
    {
        public void Configure(EntityTypeBuilder<CostItem> builder)
        {
            builder.Property(v => v.Label).IsRequired();
            builder.Property(v => v.Value).IsRequired();
            builder.HasKey(v => new {v.Label, v.CapabilityIdentifier});
            builder.ToTable("CostItem");
        }
    }
}