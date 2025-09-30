using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.Pets.DTOs;


namespace Vetsys.API.Modules.Pets.UseCases.CreatePet
{
    public class CreatePetUseCase(IPetRepository repository)
    {
        private readonly IPetRepository _repository = repository;

        // Método principal del caso de uso
        public async Task ExecuteAsync(PetCreateDTO dto)
        {
            var pet = new Pet
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                Name = dto.Name,
            };

            await _repository.AddAsync(pet);
        }
    }
}
