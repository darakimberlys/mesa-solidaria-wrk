using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MesaSolidariaWrk.Domain.Data;

namespace MesaSolidariaWrk.Repository.Configuration;

public class PackagesConfiguration: IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.ToTable("deliveries");

        builder.Property(n => n.PackageReference).HasColumnName("package_reference").HasMaxLength(255).IsRequired();
        builder.Property(n => n.Rice).HasColumnName("rice").HasColumnType("int").IsRequired();
        builder.Property(n => n.Bean).HasColumnName("bean").HasColumnType("int").IsRequired();
        builder.Property(n => n.Oil).HasColumnName("oil").HasColumnType("int").IsRequired();
        builder.Property(n => n.DeliveryStatus).HasColumnName("delivery_status").HasMaxLength(50).IsRequired();
    }
}
