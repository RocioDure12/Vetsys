using Vetsys.API.Modules.VaccinesTypes.Contracts;
using Vetsys.API.Modules.VaccinesTypes.DTOs;

namespace Vetsys.API.Modules.VaccinesTypes.UseCases.CreateVaccineType
{
    public class CreateVaccineTypeUseCase
    {
        private readonly IVaccineTypeRepository _repository;

        public CreateVaccineTypeUseCase(IVaccineTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(VaccineTypeCreateDTO dto)
        {
            var vaccineType = new VaccineType
            {
                Id = dto.Id,
                Name = dto.Name,
                FrequencyInDays = dto.FrequencyInDays
            };

            await _repository.AddAsync(vaccineType);
        }
    }
}
