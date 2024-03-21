using MassTransit;
using MesaSolidariaWrk;
using MesaSolidariaWrk.Event;
using MesaSolidariaWrk.IoC;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var fila = Environment.GetEnvironmentVariable("Subscription") ?? string.Empty;
        var conexao = Environment.GetEnvironmentVariable("PubSubConnection") ?? string.Empty;
                
        services.AddHostedService<Worker>();
        services.AddDataBaseConnection();
        services.AddServices();
         
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