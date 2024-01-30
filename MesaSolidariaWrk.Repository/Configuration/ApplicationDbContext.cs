using MesaSolidariaWrk.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace MesaSolidariaWrk.Repository.Configuration;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Delivery?> Deliveries { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeliveriesConfiguration());
        modelBuilder.ApplyConfiguration(new PackagesConfiguration());
        modelBuilder.ApplyConfiguration(new ProductsConfiguration());
    }
}
