using CostJanitor.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostJanitor.Infrastructure.EntityFramework.Configurations
{
    public class ReportItemEntityTypeConfiguration : IEntityTypeConfiguration<ReportItem>
    {
        public void Configure(EntityTypeBuilder<ReportItem> builder)
        {
            builder.Ignore(v => v.DomainEvents);
            builder.Property(v => v.Id).IsRequired();
            builder.HasKey(v => v.Id);
            builder.ToTable("ReportItem");

            builder.OwnsMany(
            p => p.CostItems, a =>
            {
                a.WithOwner().HasForeignKey("OwnerId");
                a.Property<int>("Id");
                a.HasKey("Id");
            });
        }
    }
}