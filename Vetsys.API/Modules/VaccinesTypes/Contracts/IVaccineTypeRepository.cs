namespace Vetsys.API.Modules.VaccinesTypes.Contracts
{
    public interface IVaccineTypeRepository
    {
        Task AddAsync(VaccineType vaccineType);
        Task UpdateAsync(VaccineType vaccineType);
        Task DeleteAsync(Guid id);
        Task<VaccineType?> GetByIdAsync(Guid id);
      
    }
}
