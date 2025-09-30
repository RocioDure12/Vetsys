namespace Vetsys.API.Modules.VaccinationRecords.Events
{
    public class VaccinationExpiredEvent
    {
        public Guid PetId { get; set; }
        public string ? PetName { get; set; }
        public Guid CustomerId { get; set; }
        public string ? CustomerName { get; set; }
        public string ? CustomerEmail { get; set; }
        public Guid VaccineTypeId { get; set; }
        public string ?VaccineTypeName { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
