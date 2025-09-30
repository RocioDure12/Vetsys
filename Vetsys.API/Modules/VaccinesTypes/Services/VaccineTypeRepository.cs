using Microsoft.EntityFrameworkCore;
using Vetsys.API.Data;
using Vetsys.API.Modules.VaccinesTypes.Contracts;

namespace Vetsys.API.Modules.VaccinesTypes.Services
{
    public class VaccineTypeRepository : IVaccineTypeRepository
    {
        private readonly VetsysDbContext _context;

        public VaccineTypeRepository(VetsysDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VaccineType vaccineType)
        {
            await _context.Set<VaccineType>().AddAsync(vaccineType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VaccineType vaccineType)
        {
            _context.Set<VaccineType>().Update(vaccineType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<VaccineType>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<VaccineType>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<VaccineType?> GetByIdAsync(Guid id)
        {
            return await _context.Set<VaccineType>().FindAsync(id);
        }
    }
}
