using System.Collections.Generic;

namespace Vetsys.API.Shared.Criteria
{
    public class BaseCriteria
    {
        public required List<FilterCriteria> Filters { get; init; } = new();

        // Ordenamiento
        public string? OrderBy { get; set; }
        public bool OrderByDescending { get; set; } = false;

        // Paginación
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public void AddFilter(string propertyName, object value, FilterOperator filterOperator)
        {
            Filters.Add(new FilterCriteria
            {
                PropertyName = propertyName,
                Value = value,
                Operator = filterOperator
            });
        }
    }
}
    