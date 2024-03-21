using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Domain.Data;
using MesaSolidariaWrk.Event;
using Newtonsoft.Json;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace MesaSolidariaWrk;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusReceiver _receiver;
    private readonly IConfiguration _configuration;
    private readonly IDonationService _donationService;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, IDonationService donationService)
    {
        _logger = logger;
        _configuration = configuration;
        _donationService = donationService;
        
        // TODO: Initialize Azure Service Bus client and receiver
        var fila = Environment.GetEnvironmentVariable("Subscription") ?? string.Empty;
        var conexao = Environment.GetEnvironmentVariable("PubSubConnection") ?? string.Empty;

        _serviceBusClient = new ServiceBusClient(conexao);
        _receiver = _serviceBusClient.CreateReceiver(fila);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // TODO: Receive messages from Azure Service Bus
            var receivedMessage = await _receiver.ReceiveMessageAsync(cancellationToken: stoppingToken);

            if (receivedMessage != null)
            {
                var body = receivedMessage.Body;
                var message = Encoding.UTF8.GetString(body);
                var receivedMessageData = JsonConvert.DeserializeObject<ReceivedMessageData>(message);
                if (receivedMessageData.Message != null)
                {
                    await _donationService.ProcessMessage(receivedMessageData.Message);
                }
            }

            // TODO: Complete the message to remove it from the queue
            await _receiver.CompleteMessageAsync(receivedMessage);
        }
    }
}