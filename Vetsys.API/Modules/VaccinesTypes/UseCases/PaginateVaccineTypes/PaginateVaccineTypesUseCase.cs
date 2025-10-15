using Vetsys.API.Modules.VaccinesTypes.Contracts;
using Vetsys.API.Modules.VaccinesTypes.DTOs;
using Vetsys.API.Shared.Criteria;

namespace Vetsys.API.Modules.VaccinesTypes.UseCases.PaginateVaccineTypes
{
    public class PaginateVaccineTypesUseCase
    {
        private readonly IVaccineTypeRepository _repository;

        public PaginateVaccineTypesUseCase(IVaccineTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponseDTO<VaccineType>> ExecuteAsync(BaseCriteria criteria)
        {
            var (items, count) = await _repository.FindByCriteriaAsync(criteria);
            return new PaginatedResponseDTO<VaccineType>
            {
                Count = count,
                Items = items.ToList()
            };
        }
    }
}