namespace Vetsys.API.Modules.VaccinesTypes.DTOs
{
    public class VaccineTypeCreateDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int FrequencyInDays { get; set; }
    }
}   