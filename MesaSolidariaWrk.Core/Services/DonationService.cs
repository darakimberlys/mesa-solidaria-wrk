using System.Text.Json;
using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Domain.Data;
using MesaSolidariaWrk.Repository;

namespace MesaSolidariaWrk.Core.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository _donationRepository;

    public DonationService(IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    public async Task ProcessMessage(string message)
    {
        var deserializedMessage = JsonSerializer.Deserialize<Message>(message);

        switch (deserializedMessage.MessageType)
        {
            case MessageType.PackageReceived
                : await _donationRepository.InsertPackagesAsync(deserializedMessage.Package);
                break;
            
            case MessageType.ProductsReceived
                : await _donationRepository.InsertProductsAsync(deserializedMessage.Product);
                break;
            
            case MessageType.DeliveryStatus
                : CheckDelivery(deserializedMessage.Delivery);
                break;
            
            case MessageType.DonationRequest
                : await _donationRepository.InsertDonationRequestAsync(deserializedMessage.Package);
                break;
            //good to add logs in this
        }
    }

    private async Task CheckDelivery(Delivery delivery)
    {
        var deliveryData = await _donationRepository.GetCompleteDeliveryId(delivery.PackageReference)
                           ?? new Delivery
                           {
                               Address = delivery.Address,
                               CreatedAt = DateTime.Today,
                               DeliveryStatus = DeliveryStatus.Scheduled.ToString(),
                               DeliveryDate = delivery.DeliveryDate,
                               DeliveryPlannedDate = delivery.DeliveryPlannedDate,
                               PackageReference = delivery.PackageReference,
                               Requester = delivery.Requester
                           };

       await _donationRepository.InsertDeliveryAsync(deliveryData);
       await  _donationRepository.SaveChangesForDatabase();
    }
}