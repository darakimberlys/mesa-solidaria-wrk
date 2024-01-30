namespace MesaSolidariaWrk.Domain.Data;

public class Delivery
{
    public string Requester { get; set; }
    public string PackageReference { get; set; }
    public string Address { get; set; }
    public DateTime DeliveryPlannedDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string DeliveryStatus { get; set; }
}
