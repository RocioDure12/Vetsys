using System;
using System.Threading.Tasks;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;

namespace Vetsys.API.Modules.Customers.UseCases.DeleteCustomer
{
    public class DeleteCustomerUseCase(ICustomerRepository repository)
    {
        private readonly ICustomerRepository _repository = repository;

       

        public async Task ExecuteAsync(Guid id)
        {
            var customer = await _repository.GetById(id)
                           ?? throw new KeyNotFoundException($"Customer with Id {id} not found");

            await _repository.DeleteAsync(id);
        }
        //agregar funcion que arroja la excepcion si no encuentra el customer
    }
}
