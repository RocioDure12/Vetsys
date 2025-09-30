using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vetsys.API.Modules.Customers.Contracts

{
    public interface ICustomerRepository
    {
       

        // Obtener un customer por su Id
        Task<Customer?> GetById(Guid id);

        // Listar todos los customers
        Task<IEnumerable<Customer>> GetAllAsync();

        // Agregar un nuevo customer
        Task AddAsync(Customer customer);

        // Actualizar un customer existente
        Task UpdateAsync(Customer customer);

        // Eliminar un customer por Id
        Task DeleteAsync(Guid id);
        
    }
}

