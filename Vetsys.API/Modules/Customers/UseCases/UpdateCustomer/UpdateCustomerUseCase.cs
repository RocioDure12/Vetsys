using System;
using System.Threading.Tasks;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;

namespace Vetsys.API.Modules.Customers.UseCases.DeleteCustomer
{
    public class UpdateCustomerUseCase(ICustomerRepository repository)
    {
        private readonly ICustomerRepository _repository = repository;


        //TODO:esto esta mal, corregir
        public async Task ExecuteAsync(Guid id)
        {
            var customer = await _repository.GetById(id)   
                           ?? throw new KeyNotFoundException($"Customer with Id {id} not found");
            

            await _repository.UpdateAsync(customer);
        }
    }
}
