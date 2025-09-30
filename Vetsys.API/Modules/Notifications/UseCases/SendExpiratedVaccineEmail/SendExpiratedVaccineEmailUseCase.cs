using Vetsys.API.Modules.Notifications.Contracts;
using Vetsys.API.Modules.VaccinationRecords.Events;

namespace Vetsys.API.Modules.Notifications.UseCases.SendExpiratedVaccineEmail
{
    public class SendExpiratedVaccineEmailUseCase
    {
        private readonly IEmailSender _emailService;

        public SendExpiratedVaccineEmailUseCase(IEmailSender emailService)
        {
            _emailService = emailService;
        }

        public async Task ExecuteAsync(VaccinationExpiredEvent evt)
        {
            var subject = $"Vacuna vencida para {evt.PetName}";
            var body = $"Hola {evt.CustomerName},\n\nLa mascota {evt.PetName} tiene la vacuna '{evt.VaccineTypeName}' vencida desde {evt.ExpirationDate:dd/MM/yyyy}. Favor de acudir a la veterinaria.";
            await _emailService.SendEmailAsync(evt.CustomerEmail, subject, body);
        }
    }
}