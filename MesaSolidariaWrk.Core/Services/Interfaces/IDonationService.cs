
using MesaSolidariaWrk.Domain.Data;

namespace MesaSolidariaWrk.Core.Services.Interfaces;

public interface IDonationService
{
    Task ProcessMessage(MessageData message);

}