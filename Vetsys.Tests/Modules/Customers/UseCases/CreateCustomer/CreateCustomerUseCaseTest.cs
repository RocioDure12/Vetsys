using NSubstitute;
using Vetsys.API.Modules.Customers;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Customers.DTOs;
using Vetsys.API.Modules.Customers.UseCases.CreateCustomer;
namespace Vetsys.Tests;

public class CreateCustomerUseCaseTest
{
    private ICustomerRepository _customerRepository = null!;  
    private CreateCustomerUseCase _createCustomerUseCase = null!;

    [SetUp]
    public void Setup()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _createCustomerUseCase = new CreateCustomerUseCase(_customerRepository);
    }


    [Test]
    public async Task CreateCustomer_WithValidCustomer_StoresCustomer()
    {
        // Arrange
        var customer = new CustomerCreateDTO
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        await _createCustomerUseCase.ExecuteAsync(customer);

        // Assert
        await _customerRepository
            .Received(1)
            .AddAsync(Arg.Is<Customer>(c =>
                    c.Id == customer.Id &&  
                    c.Name == customer.Name &&
                    c.LastName == customer.LastName &&  
                    c.Email == customer.Email
                )
            );
    }
}
