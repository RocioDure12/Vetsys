using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vetsys.API.Data;
using Vetsys.API.Modules.Customers.Contracts;

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

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
            
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
    }
}
