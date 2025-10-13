using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;

namespace Vetsys.API.Modules.Customers.UseCases.CreateCustomer
{
    public class CreateCustomerUseCase(ICustomerRepository repository, ILogger<CreateCustomerUseCase> logger)
    {
        private readonly ICustomerRepository _repository = repository;
        private readonly ILogger<CreateCustomerUseCase> _logger = logger;


        // Método principal del caso de uso
        public async Task ExecuteAsync(CustomerCreateDTO dto)
        {
            try
            {
                _logger.LogInformation("Creating customer with Id {CustomerId} and Email {Email}", dto.Id, dto.Email);

                await CreateCustomer(dto);

                _logger.LogInformation("Customer created with Id {CustomerId} and Email {Email}", dto.Id, dto.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer with Id {CustomerId} and Email {Email}", dto.Id, dto.Email);
                throw;
            }
        }
        public async Task CreateCustomer(CustomerCreateDTO dto)
        {
            var customer = new Customer
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
            };
            await _repository.AddAsync(customer);
        }
    }
}
