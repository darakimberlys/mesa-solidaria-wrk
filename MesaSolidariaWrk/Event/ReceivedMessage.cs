using MassTransit;
using MesaSolidariaWrk.Core.Services;
using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Domain.Data;
using MesaSolidariaWrk.Repository;

namespace MesaSolidariaWrk.Event;

    public class ReceivedMessage : IConsumer<MessageData>
    {
        public Task Consume(ConsumeContext<MessageData> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
