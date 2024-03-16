using Azure.Messaging.ServiceBus;
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

    public static void AddPubSubConfiguration(this IServiceCollection services)
    {
        var fila = Environment.GetEnvironmentVariable("Subscription") ?? string.Empty;
        var conexao = Environment.GetEnvironmentVariable("PubSubConnection") ?? string.Empty;

        services.AddSingleton<ServiceBusReceiver>(serviceProvider =>
        {
            var client = new ServiceBusClient(conexao);
            var receiver = client.CreateReceiver(fila);
            return receiver;
        });

        services.AddHostedService<Worker>();

    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDonationRepository, DonationRepository>();
        serviceCollection.AddScoped<IDonationService, DonationService>();
    }
}