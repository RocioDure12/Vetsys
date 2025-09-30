namespace Vetsys.API.Modules.VaccinesTypes
{
    public class VaccineType
    {
        public Guid Id { get; set; }  // Identificador único

        public string Name { get; set; } = string.Empty; // Nombre de la vacuna (ej: Rabia, Moquillo, etc.)

        public int FrequencyInDays { get; set; } // Frecuencia en días (ej: 365 = una vez al año)
    }
}
