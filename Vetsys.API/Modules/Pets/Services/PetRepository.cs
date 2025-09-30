using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vetsys.API.Data;
using Vetsys.API.Modules.Pets.Contracts;

namespace Vetsys.API.Modules.Pets.Services
{
    public class PetRepository : IPetRepository
    {
        private readonly VetsysDbContext _context;
        public PetRepository(VetsysDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Pet pet)

        {
            await _context.AddAsync(pet);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var pet=await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Remove(pet);
                await _context.SaveChangesAsync();
            }
             
     
        }

        public Task<IEnumerable<Pet>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pet>> GetByCustomerIdAsync(Guid customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Pets)
                .FirstOrDefaultAsync(c => c.Id == customerId)
                ?? throw new NotImplementedException($"No se encontró el cliente con Id {customerId}");

            return customer.Pets;

        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await _context.Pets.FindAsync(id);   

        }

        public Task<IEnumerable<Pet>> GetExpiredVaccines()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }
    }
}
