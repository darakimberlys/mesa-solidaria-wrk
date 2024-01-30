using MesaSolidariaWrk.Domain.Data;
using MesaSolidariaWrk.Repository.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MesaSolidariaWrk.Repository;

public class DonationRepository : IDonationRepository
{
    private readonly ApplicationDbContext _context;

    public DonationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EntityEntry<Package>> InsertPackagesAsync(Package packageToRegister)
    {
        return await _context.Packages.AddAsync(packageToRegister);
    }

    public async Task<EntityEntry<Product>> InsertProductsAsync(Product productToRegister)
    {
        return await _context.Products.AddAsync(productToRegister);
    }

    public async Task<EntityEntry<Package>> InsertDonationRequestAsync(Package packageToRegister)
    {
        return await _context.Packages.AddAsync(packageToRegister);
    }
    
    public async Task<Delivery?> GetCompleteDeliveryId(string id)
    {
        return await _context.Deliveries.FindAsync(id);
    }
    
    public async Task<EntityEntry<Delivery>> InsertDeliveryAsync(Delivery delivery)
    {
        return await _context.Deliveries.AddAsync(delivery);
    }
    
    public async Task<int> SaveChangesForDatabase()
    {
        return await _context.SaveChangesAsync();
    }
}