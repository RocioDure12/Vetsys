using Vetsys.API.Shared.Criteria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vetsys.API.Modules.Customers.Contracts
{
    public interface ICustomerRepository
    {
        // Obtener un customer por su Id
        Task<Customer?> GetById(Guid id);

        // Agregar un nuevo customer
        Task AddAsync(Customer customer);

        // Actualizar un customer existente
        Task UpdateAsync(Customer customer);

        // Eliminar un customer por Id
        Task DeleteAsync(Guid id);

        // Nuevo método para búsqueda por criterios dinámicos
        Task<IEnumerable<Customer>> FindByCriteriaAsync(BaseCriteria criteria);
     

        // Contar customers según criterios dinámicos
        Task<int> CountByCriteriaAsync(BaseCriteria criteria);
    }
}

