using MassTransit;
using MesaSolidariaWrk.Domain.Data;

namespace MesaSolidariaWrk.Event;

    public class ReceivedMessage : IConsumer<MessageData>
    {
        public Task Consume(ConsumeContext<MessageData> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
