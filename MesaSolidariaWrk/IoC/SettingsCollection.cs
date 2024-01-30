using MassTransit;
using MesaSolidariaWrk.Core.Services;
using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Event;
using MesaSolidariaWrk.Repository;
using MesaSolidariaWrk.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MesaSolidariaWrk.IoC;

public static class SettingsCollection
{
    public static void AddDataBaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<ApplicationDbContext>(builder => 
            builder.UseMySql(connectionString,
            MySqlServerVersion.LatestSupportedServerVersion));
    }

    public static void AddPubSubConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var fila = configuration.GetSection("MassTransitAzure")["Subscription"] ?? string.Empty;
        var conexao = configuration.GetSection("MassTransitAzure")["PubSubConnection"] ?? string.Empty;

        services.AddMassTransit(x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(conexao);

                cfg.ReceiveEndpoint(fila, e => { e.Consumer<ReceivedMessage>(); });
            });
        });
    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDonationRepository, DonationRepository>();
        serviceCollection.AddScoped<IDonationService, DonationService>();
    }
}