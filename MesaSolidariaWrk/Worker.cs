using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MesaSolidariaWrk.Domain.Data;
using MesaSolidariaWrk.Event;
using Message = Microsoft.Azure.ServiceBus.Message;

namespace MesaSolidariaWrk;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusReceiver _receiver;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

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
                var pedido = JsonSerializer.Deserialize<ReceivedMessageData>(message);
                if (pedido.Message != null)
                {
                    Console.WriteLine(pedido.Message.MessageType.ToString());
                }
            }

            // TODO: Complete the message to remove it from the queue
            await _receiver.CompleteMessageAsync(receivedMessage);
        }
    }
}