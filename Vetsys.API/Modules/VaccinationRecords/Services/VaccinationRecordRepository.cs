using Microsoft.EntityFrameworkCore;
using Vetsys.API.Data;
using Vetsys.API.Modules.VaccinationRecords;
using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.DTOs;
namespace Vetsys.API.Modules.VaccinationRecords.Services
{
    public class VaccinationRecordRepository : IVaccinationRecordRepository
    {
        private readonly VetsysDbContext _context;
   

        public VaccinationRecordRepository(VetsysDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(VaccinationRecord vaccinationRecord)
        {
            await _context.AddAsync(vaccinationRecord);
            await _context.SaveChangesAsync();
           
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<VaccinationRecord> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpiredVaccineDTO>> GetExpiredVaccinesAsync()
        {
            var today = DateTime.UtcNow;
            //TODO: Optimizar consulta
            var query = await _context.VaccinationRecords
                .GroupBy(v => new { v.PetId, v.VaccineTypeId })
                .Select(g => new ExpiredVaccineDTO
                {
                    PetId = g.Key.PetId,
                    VaccineTypeId = g.Key.VaccineTypeId,
                    ExpirationDate = g.Max(v => v.ExpirationDate),
                    VaccineType = g.Select(v => v.VaccineType).FirstOrDefault()!
                })
                .Where(x => x.ExpirationDate.AddDays(-30) <= today)
                .ToListAsync();

            return query;
        }


        public Task UpdateAsync(VaccinationRecord VaccinationRecord)
        {
            throw new NotImplementedException();
        }
    }
}
