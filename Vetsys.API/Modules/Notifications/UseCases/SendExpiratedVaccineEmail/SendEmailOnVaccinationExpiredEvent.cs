using System.Threading.Tasks;
using MassTransit;
using Vetsys.API.Modules.VaccinationRecords.Events;

namespace Vetsys.API.Modules.Notifications.UseCases.SendExpiratedVaccineEmail
{
    public class SendEmailOnVaccinationExpiredEvent : IConsumer<VaccinationExpiredEvent>
    {
        private readonly SendExpiratedVaccineEmailUseCase _sendExpiratedEmailUseCase;

        public SendEmailOnVaccinationExpiredEvent(SendExpiratedVaccineEmailUseCase sendExpiratedEmailUseCase)
        {
            _sendExpiratedEmailUseCase = sendExpiratedEmailUseCase;
        }

        public async Task Consume(ConsumeContext<VaccinationExpiredEvent> context)
        {
            await _sendExpiratedEmailUseCase.ExecuteAsync(context.Message);
        }
    }
}
