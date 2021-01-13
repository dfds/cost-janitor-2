using CostJanitor.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostJanitor.Infrastructure.EntityFramework.Configurations
{
    public class CostItemReferenceEntityTypeConfiguration : IEntityTypeConfiguration<CostItemReference>
    {
        public void Configure(EntityTypeBuilder<CostItemReference> builder)
        {
            builder.Ignore(v => v.DomainEvents);
            builder.HasKey(v => v.Id);
        }
    }
}