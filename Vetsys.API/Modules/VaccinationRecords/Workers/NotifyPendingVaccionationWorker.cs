using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vetsys.API.Modules.VaccinationRecords.UseCases.FindPendingVaccination;


namespace Vetsys.API.Modules.VaccinationRecords.Workers
{
    public class NotifyPendingVaccionationWorker : BackgroundService
    {
        private readonly ILogger<NotifyPendingVaccionationWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;


        public NotifyPendingVaccionationWorker(
            ILogger<NotifyPendingVaccionationWorker> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Ejecutando notificación de vacunas pendientes: {time}", DateTimeOffset.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var notifyPendingVaccinationUseCase = scope.ServiceProvider.GetRequiredService<FindPendingVaccinationUseCase>();
                    await notifyPendingVaccinationUseCase.ExecuteAsync();
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Ejecuta cada 24 horas
            }
        }
    }
}
