using Vetsys.API.Shared.Criteria;

namespace Vetsys.API.Modules.VaccinesTypes.Contracts
{
    public interface IVaccineTypeRepository
    {
    
        Task AddAsync(VaccineType vaccineType);
        Task UpdateAsync(VaccineType vaccineType);
        Task DeleteAsync(Guid id);
        Task<VaccineType?> GetByIdAsync(Guid id);
        Task<(IEnumerable<VaccineType> Items, int Count)> FindByCriteriaAsync(BaseCriteria criteria);
      
    }
}
