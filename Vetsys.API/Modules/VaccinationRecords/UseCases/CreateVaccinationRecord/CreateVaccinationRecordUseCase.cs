using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.DTOs;

namespace Vetsys.API.Modules.VaccinationRecords.UseCases.CreateVaccinationRecord
{
    public class CreateVaccinationRecordUseCase(
        IVaccinationRecordRepository repository, 
        ILogger<CreateVaccinationRecordUseCase> logger
    )
    {
        private readonly IVaccinationRecordRepository _repository = repository;
        private readonly ILogger<CreateVaccinationRecordUseCase> _logger = logger;

        public async Task ExecuteAsync(VaccinationRecordCreateDTO dto)
        {
            try
            {
                _logger.LogInformation(
                    "Creating vaccination record for PetId {PetId} with VaccineTypeId {VaccineTypeId}", 
                    dto.PetId, 
                    dto.VaccineTypeId
                );
                await CreateVaccinationRecord(dto);
                _logger.LogInformation(
                    "Vaccination record created for PetId {PetId} with VaccineTypeId {VaccineTypeId}", 
                    dto.PetId, 
                    dto.VaccineTypeId
                );  
            }
            catch(Exception ex)
            {
                _logger.LogError(
                    ex, 
                    "Error creating vaccination record for PetId {PetId} with VaccineTypeId {VaccineTypeId}", 
                    dto.PetId, 
                    dto.VaccineTypeId
                );
                throw;
            }
        }

        private async Task CreateVaccinationRecord(VaccinationRecordCreateDTO dto)
        {
            var vaccinationRecord = new VaccinationRecord
            {
                Id = dto.Id,
                PetId = dto.PetId,
                VaccineTypeId = dto.VaccineTypeId,
                DateAdministered = dto.DateAdministered,
                ExpirationDate = dto.ExpirationDate,

            };
            await _repository.AddAsync(vaccinationRecord);
        }
    }
}

