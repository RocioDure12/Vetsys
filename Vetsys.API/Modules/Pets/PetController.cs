using Microsoft.AspNetCore.Mvc;
using Vetsys.API.Modules.Pets.Contracts;
using Vetsys.API.Modules.Pets.DTOs;
using Vetsys.API.Modules.Pets.UseCases.CreatePet;

namespace Vetsys.API.Modules.Pets
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController(IPetRepository repository): ControllerBase
    {
        private readonly CreatePetUseCase _createPetUseCase = new(repository);

      

        [HttpPost]
        public async Task<IActionResult> Create(PetCreateDTO pet)
        {
            await _createPetUseCase.ExecuteAsync(pet);
            return Created();
        }
    }
}
