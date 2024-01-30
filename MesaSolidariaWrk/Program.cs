using MesaSolidariaWrk;
using MesaSolidariaWrk.IoC;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.AddScoped<Worker>();
        services.AddServices();
        services.AddDataBaseConnection(configuration);
        services.AddPubSubConfiguration(configuration);
    })
    .Build();

host.Run();