using System.Linq.Dynamic.Core;

namespace Vetsys.API.Shared.Criteria
{
    public class CriteriaTranslatorEFCore
    {
        public static IQueryable<T> ApplyCriteria<T>(IQueryable<T> query, BaseCriteria criteria)
        {
            // Filtros
            foreach (var filter in criteria.Filters)
            {
                string expression = filter.Operator switch
                {
                    FilterOperator.Equals => $"{filter.PropertyName} == @0",
                    FilterOperator.NotEquals => $"{filter.PropertyName} != @0",
                    FilterOperator.GreaterThan => $"{filter.PropertyName} > @0",
                    FilterOperator.LessThan => $"{filter.PropertyName} < @0",
                    FilterOperator.In => $"@0.Contains({filter.PropertyName})",
                    FilterOperator.NotIn => $"!@0.Contains({filter.PropertyName})",
                    // Puedes agregar más operadores aquí
                    _ => throw new NotSupportedException($"Operador {filter.Operator} no soportado")
                    //TODO: Arrojar excepción personalizada (de dominio)
                };

                query = query.Where(expression, filter.Value);
            }

            // Ordenamiento
            if (!string.IsNullOrWhiteSpace(criteria.OrderBy))
            {
                var orderExpr = criteria.OrderByDescending
                    ? $"{criteria.OrderBy} descending"
                    : criteria.OrderBy!;
                query = query.OrderBy(orderExpr);
            }

            // Paginación
            if (criteria.PageNumber.HasValue && criteria.PageSize.HasValue)
            {
                int skip = criteria.PageNumber.Value * criteria.PageSize.Value;
                query = query.Skip(skip).Take(criteria.PageSize.Value);
            }

            return query;
        }
    }
}
