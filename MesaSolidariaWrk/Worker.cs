using System.Text;
using Azure.Messaging.ServiceBus;
using MesaSolidariaWrk.Core.Services.Interfaces;

namespace MesaSolidariaWrk;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusReceiver _receiver;
    private readonly IConfiguration _configuration;
    private readonly IDonationService _donationService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, 
        IConfiguration configuration, 
        IDonationService donationService,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _donationService = donationService;
        _serviceScopeFactory = serviceScopeFactory;

        var fila = configuration.GetSection("MassTransitAzure")["Subscription"] ?? string.Empty;
        var conexao = configuration.GetSection("MassTransitAzure")["Connection"] ?? string.Empty;

        _serviceBusClient = new ServiceBusClient(conexao);
        _receiver = _serviceBusClient.CreateReceiver(fila);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var donationService = scope.ServiceProvider.GetRequiredService<IDonationService>();
            // Use donationService dentro deste escopo

            while (!stoppingToken.IsCancellationRequested)
            {
                var receivedMessage = await _receiver.ReceiveMessageAsync(cancellationToken: stoppingToken);

                if (receivedMessage != null)
                {
                    var body = receivedMessage.Body;
                    var message = Encoding.UTF8.GetString(body);

                    await _donationService.ProcessMessage(message);
                }

                await _receiver.CompleteMessageAsync(receivedMessage, stoppingToken);
            }
        }
    }
}