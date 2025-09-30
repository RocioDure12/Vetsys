using System;
using System.Threading.Tasks;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;

namespace Vetsys.API.Modules.Customers.UseCases.CreateCustomer
{
    public class CreateCustomerUseCase(ICustomerRepository repository)
    {
        private readonly ICustomerRepository _repository = repository;

        // Método principal del caso de uso
        public async Task ExecuteAsync(CustomerCreateDTO dto)
        {
            var customer = new Customer
            {
                Id =dto.Id,  
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
             
            };

            await _repository.AddAsync(customer);
          
        }
       

    }
}
