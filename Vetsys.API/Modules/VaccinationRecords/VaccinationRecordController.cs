using Microsoft.AspNetCore.Mvc;
using Vetsys.API.Modules.VaccinationRecords.DTOs;
using Vetsys.API.Modules.VaccinationRecords.UseCases.CreateVaccinationRecord;
using Vetsys.API.Modules.VaccinationRecords.UseCases.FindPendingVaccination;

namespace Vetsys.API.Modules.VaccinationRecords
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationRecordController : ControllerBase
    {
        private readonly CreateVaccinationRecordUseCase _createVaccinationRecordUseCase;
        private readonly FindPendingVaccinationUseCase _notifyPendingVaccinationUseCase;

        public VaccinationRecordController(
            CreateVaccinationRecordUseCase createVaccinationRecordUseCase,
            FindPendingVaccinationUseCase notifyPendingVaccinationUseCase)
        {
            _createVaccinationRecordUseCase = createVaccinationRecordUseCase;
            _notifyPendingVaccinationUseCase = notifyPendingVaccinationUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(VaccinationRecordCreateDTO dto)
        {
            await _createVaccinationRecordUseCase.ExecuteAsync(dto);
            return Created();
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingVaccinations()
        {
            await _notifyPendingVaccinationUseCase.ExecuteAsync();
            return Ok();
        }

        
    }
}
