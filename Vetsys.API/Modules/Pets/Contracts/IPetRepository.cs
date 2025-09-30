namespace Vetsys.API.Modules.Pets.Contracts
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetExpiredVaccines();
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pet>> GetByCustomerIdAsync(Guid customerId);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(Guid id);
    }
}
