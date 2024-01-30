namespace MesaSolidariaWrk.Domain.Data;

public class Package
{
    public string Donator { get; set; }
    public string Requester { get; set; }
    public string PackageReference { get; set; }
    public int Rice { get; set; }
    public int Bean { get; set; }
    public int Oil { get; set; }
    public string DeliveryStatus { get; set; }
}