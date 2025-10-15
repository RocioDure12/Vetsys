namespace Vetsys.API.Modules.VaccinesTypes.DTOs
{
    public class PaginatedResponseDTO<T>
    {
        public int Count { get; set; }
        public List<T> Items { get; set; } = [];
    }
}
