    namespace Vetsys.API.Modules.Pets.DTOs
{
    public class PetCreateDTO
    {
        public required Guid Id { get; set; } // generado por el backend
        public required Guid CustomerId { get; set; } // a qué cliente pertenece
        public required string Name { get; set; } = string.Empty;
    

    }
}
