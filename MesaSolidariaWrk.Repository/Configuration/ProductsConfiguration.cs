using MesaSolidariaWrk.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MesaSolidariaWrk.Repository.Configuration;

public class ProductsConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("deliveries");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Rice).HasColumnName("rice").HasColumnType("int").IsRequired();
        builder.Property(n => n.Bean).HasColumnName("bean").HasColumnType("int").IsRequired();
        builder.Property(n => n.Oil).HasColumnName("oil").HasColumnType("int").IsRequired();
    }
}
