using MassTransit;
using MesaSolidariaWrk.Domain.Data;

namespace MesaSolidariaWrk.Event;

    public class ReceivedMessage : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
