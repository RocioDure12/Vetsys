using MassTransit;
using NSubstitute;
using Vetsys.API.Modules.Customers;
using Vetsys.API.Modules.Customers.Contracts;
using Vetsys.API.Modules.Pets;
using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.VaccinationRecords.Contracts;
using Vetsys.API.Modules.VaccinationRecords.DTOs;
using Vetsys.API.Modules.VaccinationRecords.Events;
using Vetsys.API.Modules.VaccinationRecords.UseCases.FindPendingVaccination;
using Vetsys.API.Modules.VaccinesTypes;

namespace Vetsys.Tests;

public class FindPendingVaccinationUseCaseTest
{
    private IVaccinationRecordRepository _repository = null!;
    private IPetRepository _petRepository = null!;
    private ICustomerRepository _customerRepository = null!;
    private IPublishEndpoint _publishEndpoint = null!;
    private FindPendingVaccinationUseCase _useCase = null!;

    [SetUp]
    public void Setup()
    {
        // Arrange: crea los mocks
        _repository = Substitute.For<IVaccinationRecordRepository>();
        _petRepository = Substitute.For<IPetRepository>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _publishEndpoint = Substitute.For<IPublishEndpoint>();

        _useCase = new FindPendingVaccinationUseCase(
            _repository,
            _petRepository,
            _customerRepository,
            _publishEndpoint);
    }

    [Test]
    public async Task ExecuteAsync_WhenThereAreExpiredVaccines_PublishesEvents()
    {
        // Arrange: configura los datos de prueba en los mocks
        var petId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var VaccineTypeId = Guid.NewGuid();
        var expirationDate = DateTime.UtcNow.AddDays(-1);

        var expiratedVaccine = new ExpiredVaccineDTO
        {
            PetId = petId,
            VaccineTypeId = VaccineTypeId,
            ExpirationDate = expirationDate,
            VaccineType = new VaccineType { Id = VaccineTypeId, Name = "Rabia" }
        };

        var pet = new Pet
        {
            Id = petId,
            Name = "Fido",
            CustomerId = customerId
        };

        var customer = new Customer
        {
            Id = customerId,
            Name = "John Doe",
            LastName = "Perez",
            Email = "juan.perez@email.com"
        };
        _repository.GetExpiredVaccinesAsync().Returns(new List<ExpiredVaccineDTO> { expiratedVaccine });
        _petRepository.GetByIdAsync(petId).Returns(pet);
        _customerRepository.GetById(customerId).Returns(customer);



        // Act: ejecuta el caso de uso
        await _useCase.ExecuteAsync();

        // Assert: verifica que se haya llamado a Publish y a los repositorios
        await _publishEndpoint.Received(1).Publish(Arg.Is<VaccinationExpiredEvent>(e =>
            e.PetId == petId &&
            e.PetName == "Fido" &&
            e.CustomerId == customerId &&
            e.CustomerName == "John Doe" &&
            e.CustomerEmail == "juan.perez@email.com" &&
            e.VaccineTypeId == VaccineTypeId &&
            e.VaccineTypeName == "Rabia" &&
            e.ExpirationDate == expirationDate
)
);

    }
}
