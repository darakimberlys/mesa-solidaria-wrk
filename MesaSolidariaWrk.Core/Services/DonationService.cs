using MesaSolidariaWrk.Core.Services.Interfaces;
using MesaSolidariaWrk.Domain.Data;
using MesaSolidariaWrk.Repository;
using Microsoft.Extensions.Logging;

namespace MesaSolidariaWrk.Core.Services;

public class DonationService : IDonationService
{
    private readonly IDonationRepository _donationRepository;
    private readonly ILogger<DonationService> _logger;

    public DonationService(ILogger<DonationService> logger , IDonationRepository donationRepository)
    {
        _logger = logger;
        _donationRepository = donationRepository;
    }

    public async Task ProcessMessageAsync(MessageData message)
    {
        switch (message.MessageType)
        {
            case MessageType.PackageReceived
                : _logger.LogInformation("New package received!");
                await _donationRepository.InsertPackagesAsync(message.Package);
                break;
            
            case MessageType.ProductsReceived
                : _logger.LogInformation("New product received!");
                await _donationRepository.InsertProductsAsync(message.Product);
                break;
            
            case MessageType.DeliveryStatus
                : _logger.LogInformation("New deliveryStatus received!");
                CheckDelivery(message.Delivery);
                break;
            
            case MessageType.DonationRequest
                : _logger.LogInformation("New donation request received!");
                await _donationRepository.InsertDonationRequestAsync(message.Package);
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