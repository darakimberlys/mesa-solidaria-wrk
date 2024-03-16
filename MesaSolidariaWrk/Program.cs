using MassTransit;
using MesaSolidariaWrk;
using MesaSolidariaWrk.Core.Services;
using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Event;
using MesaSolidariaWrk.Repository;
using MesaSolidariaWrk.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        
        var configuration = hostContext.Configuration;
        var fila = Environment.GetEnvironmentVariable("Subscription") ?? string.Empty;
        var conexao = Environment.GetEnvironmentVariable("PubSubConnection") ?? string.Empty;
        services.AddHostedService<Worker>();
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<ApplicationDbContext>(builder => 
            builder.UseMySql(connectionString,
                MySqlServerVersion.LatestSupportedServerVersion));
        
        services.AddScoped<IDonationRepository, DonationRepository>();
        services.AddScoped<IDonationService, DonationService>();

        services.AddMassTransit(x =>
        {
            x.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(conexao);

                cfg.ReceiveEndpoint(fila, e =>
                {
                    e.Consumer<ReceivedMessage>();
                });
            });
        });
    })
    .Build();

host.Run();