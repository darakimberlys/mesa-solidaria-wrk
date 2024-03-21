using System.Text;
using Azure.Messaging.ServiceBus;
using MesaSolidariaWrk.Domain.Data;
using Newtonsoft.Json;

namespace MesaSolidariaWrk;

public class Worker : BackgroundService, IHostedService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusReceiver _receiver;
   // private readonly IDonationService _donationService;

    public Worker(ILogger<Worker> logger
            //  , IDonationService donationService
    )
    {
        _logger = logger;
      //  _donationService = donationService;
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
                    Console.WriteLine($"Message received: {receivedMessageData.Message.MessageType}, Sending to process!");
                    //await _donationService.ProcessMessage(receivedMessageData.Message);
                }
            }

            // TODO: Complete the message to remove it from the queue
            await _receiver.CompleteMessageAsync(receivedMessage);
        }
    }
}