using Vetsys.API.Modules.Notifications.Contracts;

namespace Vetsys.API.Modules.Notifications.UseCases.SendExpiratedVaccineTelegram
{
    public class SendExpiratedVaccineTelegramUseCase
    {
        private readonly ITelegramSender _telegramService;

        public SendExpiratedVaccineTelegramUseCase(ITelegramSender telegramService)
        {
            _telegramService = telegramService;
        }

        public async Task ExecuteAsync(string ownerTelegramId, string petName, DateTime lastVaccinationDate)
        {
            var message = $"La mascota {petName} tiene la vacuna vencida desde {lastVaccinationDate:dd/MM/yyyy}. Favor de acudir a la veterinaria.";
            await _telegramService.SendTelegramMessageAsync(ownerTelegramId, message);
        }
    }
}
