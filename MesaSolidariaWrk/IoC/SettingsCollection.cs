using MesaSolidariaWrk.Core.Services;
using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Repository;
using MesaSolidariaWrk.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MesaSolidariaWrk.IoC;

public static class SettingsCollection
{
    public static void AddDataBaseConnection(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("Connection");

        services.AddDbContext<ApplicationDbContext>(builder => 
            builder.UseMySql(connectionString,
            MySqlServerVersion.LatestSupportedServerVersion));
    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDonationRepository, DonationRepository>();
        serviceCollection.AddScoped<IDonationService, DonationService>();
    }
}