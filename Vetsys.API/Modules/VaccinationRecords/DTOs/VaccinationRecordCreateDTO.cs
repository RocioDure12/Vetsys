namespace Vetsys.API.Modules.VaccinationRecords.DTOs
{
    public class VaccinationRecordCreateDTO
    {
        public Guid Id;
        public  Guid PetId { get; set; }           // A qué mascota pertenece
        public  Guid VaccineTypeId { get; set; }   // Tipo de vacuna
        public DateTime DateAdministered { get; set; } // Fecha de aplicación
        public DateTime ExpirationDate { get; set; }   // Fecha de vencimiento
    }
}

