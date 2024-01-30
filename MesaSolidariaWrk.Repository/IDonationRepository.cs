using MesaSolidariaWrk.Domain.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MesaSolidariaWrk.Repository;

public interface IDonationRepository
{
    Task<EntityEntry<Package>> InsertPackagesAsync(Package packageToRegister);
    Task<EntityEntry<Product>> InsertProductsAsync(Product productToRegister);
    Task<EntityEntry<Package>> InsertDonationRequestAsync(Package packageToRegister);
    Task<Delivery?> GetCompleteDeliveryId(string id);
    Task<EntityEntry<Delivery>> InsertDeliveryAsync(Delivery delivery);
    Task<int> SaveChangesForDatabase();
}