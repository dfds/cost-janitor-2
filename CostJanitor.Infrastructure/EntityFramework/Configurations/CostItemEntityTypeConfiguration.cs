using CostJanitor.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostJanitor.Infrastructure.EntityFramework.Configurations
{
    public class CostItemEntityTypeConfiguration : IEntityTypeConfiguration<CostItem>
    {
        public void Configure(EntityTypeBuilder<CostItem> builder)
        {
            builder.Ignore(v => v.DomainEvents);
            builder.Property(v => v.Label).IsRequired();
            builder.Property(v => v.Value).IsRequired();
            builder.HasKey(v => v.Id);
            builder.ToTable("CostItem");
            
        }
    }
}