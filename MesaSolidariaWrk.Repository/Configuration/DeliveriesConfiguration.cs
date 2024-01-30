using MesaSolidariaWrk.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MesaSolidariaWrk.Repository.Configuration;

public class DeliveriesConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("deliveries");
        
        builder.Property(n => n.PackageReference).HasColumnName("package_reference").HasMaxLength(255).IsRequired();
        builder.Property(n => n.Address).HasColumnName("address").HasMaxLength(255).IsRequired();
        builder.Property(n => n.DeliveryDate).HasColumnName("delivery_date").HasColumnType("datetime").IsRequired();
        builder.Property(n => n.DeliveryPlannedDate).HasColumnName("delivery_planned_date").HasColumnType("datetime").IsRequired();
        builder.Property(n => n.DeliveryStatus).HasColumnName("delivery_status").HasMaxLength(50).IsRequired();
    }
}
