using System;
using System.Threading.Tasks;
using Vetsys.API.Modules.Pets;
using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.Pets.DTOs;
using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.DTOs;

namespace Vetsys.API.Modules.VaccinationRecords.UseCases.CreateVaccinationRecord
{
    public class CreateVaccinationRecordUseCase(IVaccinationRecordRepository repository)
    {
        private readonly IVaccinationRecordRepository _repository = repository;

        public async Task ExecuteAsync(VaccinationRecordCreateDTO dto)
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

