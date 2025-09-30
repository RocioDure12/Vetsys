using Microsoft.AspNetCore.Mvc;
using Vetsys.API.Modules.VaccinesTypes.Contracts;
using Vetsys.API.Modules.VaccinesTypes.DTOs;
using Vetsys.API.Modules.VaccinesTypes.UseCases.CreateVaccineType;

namespace Vetsys.API.Modules.VaccinesTypes
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccineTypeController(IVaccineTypeRepository repository): ControllerBase
    {
        private readonly CreateVaccineTypeUseCase _createVaccineTypeUseCase = new(repository);

    
        [HttpPost]
        public async Task<IActionResult> Create(VaccineTypeCreateDTO dto)
        {
            await _createVaccineTypeUseCase.ExecuteAsync(dto);
            return Created();
        }
    }
}
