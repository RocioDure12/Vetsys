using System.Collections.Generic;
using System.Threading.Tasks;
using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.DTOs;
using Vetsys.API.Modules.VaccinationRecords.Events;
using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Notifications.UseCases.SendExpiratedVaccineEmail;
using MassTransit;

namespace Vetsys.API.Modules.VaccinationRecords.UseCases.FindPendingVaccination
{
    public class FindPendingVaccinationUseCase
    {
        private readonly IVaccinationRecordRepository _repository;
        private readonly IPetRepository _petRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public FindPendingVaccinationUseCase(
            IVaccinationRecordRepository repository,
            IPetRepository petRepository,
            ICustomerRepository customerRepository,
            IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _petRepository = petRepository;
            _customerRepository = customerRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task ExecuteAsync()
        {
            var expiredVaccines = await _repository.GetExpiredVaccinesAsync();
            foreach (var dto in expiredVaccines)
            {
                var pet = await _petRepository.GetByIdAsync(dto.PetId);
                var customer = pet != null ? await _customerRepository.GetById(pet.CustomerId) : null;

                var evt = new VaccinationExpiredEvent
                {
                    PetId = dto.PetId,
                    PetName = pet?.Name ?? string.Empty,
                    CustomerId = pet?.CustomerId ?? Guid.Empty,
                    CustomerName = customer?.Name ?? string.Empty,
                    CustomerEmail = customer?.Email ?? string.Empty,
                    VaccineTypeId = dto.VaccineTypeId,
                    VaccineTypeName = dto.VaccineType?.Name ?? string.Empty,
                    ExpirationDate = dto.ExpirationDate
                };

                await _publishEndpoint.Publish(evt);
            }
        }
    }
}