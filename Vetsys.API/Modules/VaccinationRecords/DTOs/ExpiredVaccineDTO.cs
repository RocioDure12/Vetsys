using Vetsys.API.Modules.VaccinesTypes;

namespace Vetsys.API.Modules.VaccinationRecords.DTOs
{
    public class ExpiredVaccineDTO
    {
        public required Guid PetId { get; set; }
        public required Guid VaccineTypeId { get; set; }
        public required DateTime ExpirationDate { get; set; } // la fecha máxima (vencida)
        public required VaccineType VaccineType { get;set; }
       
    }
}
