namespace MesaSolidariaWrk.Domain.Data;

public class MessageData
{
    public MessageType MessageType { get; set; }
    public Package Package { get; set; }
    public Product Product { get; set; }
    public Delivery Delivery { get; set; }
}