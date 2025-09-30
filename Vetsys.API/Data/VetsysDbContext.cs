using Microsoft.EntityFrameworkCore;
using Vetsys.API.Modules.Customers;
using Vetsys.API.Modules.Pets;
using Vetsys.API.Modules.VaccinationRecords;
using Vetsys.API.Modules.VaccinesTypes;

namespace Vetsys.API.Data
{
    public class VetsysDbContext : DbContext
    {
        public VetsysDbContext(DbContextOptions<VetsysDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Pet> Pets { get; set; } = default!;

        public DbSet<VaccinationRecord> VaccinationRecords { get; set; } = default!;

        public DbSet<VaccineType> VaccineTypes { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
