using Microsoft.AspNetCore.Mvc;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;
using Vetsys.API.Modules.Customers.Services;
using Vetsys.API.Modules.Customers.UseCases.CreateCustomer;
using Vetsys.API.Modules.Customers.UseCases.DeleteCustomer;




namespace Vetsys.API.Modules.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ICustomerRepository repository) : ControllerBase
    {
        private readonly CreateCustomerUseCase _createCustomerUseCase = new(repository);
        private readonly DeleteCustomerUseCase _deleteCustomerUseCase = new(repository);

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDTO customer)
        {
            await _createCustomerUseCase.ExecuteAsync(customer);
            return Created();
              
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteCustomerUseCase.ExecuteAsync(id);
            return NoContent();
        }

        
    }
}
