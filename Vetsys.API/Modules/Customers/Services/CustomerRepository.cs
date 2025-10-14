using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vetsys.API.Data;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Shared.Criteria; // <-- Importante para BaseCriteria y CriteriaTranslatorEFCore

namespace Vetsys.API.Modules.Customers.Services
{
    public class CustomerRepository(VetsysDbContext context) : ICustomerRepository
    {

        private readonly VetsysDbContext _context = context;

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }


        public async Task UpdateAsync(Customer customer)
        {
             _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        // Nuevo método para búsqueda por criterios dinámicos
        public async Task<IEnumerable<Customer>> FindByCriteriaAsync(BaseCriteria criteria)
        {
            var query = _context.Customers.AsQueryable();
            query = CriteriaTranslatorEFCore.ApplyCriteria(query, criteria);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<int> CountByCriteriaAsync(BaseCriteria criteria)
        {
            var query = _context.Customers.AsQueryable();
            query = CriteriaTranslatorEFCore.ApplyCriteria(query, criteria);
            return await query.CountAsync();
        }
    }
}
