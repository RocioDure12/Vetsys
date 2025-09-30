using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vetsys.API.Modules.VaccinationRecords.DTOs;
namespace Vetsys.API.Modules.VaccinationRecords.Contracts;

    public interface IVaccinationRecordRepository
    {


        // Obtener registro de vacunacion por su Id
        Task<VaccinationRecord> GetByIdAsync(Guid id);

        //Obtener vacuna vencidas agrupadas por tipo de vacuna y mascota
        Task<IEnumerable<ExpiredVaccineDTO>> GetExpiredVaccinesAsync();


        // Agregar un nuevo registro de vacunacion
        Task AddAsync(VaccinationRecord vaccinationRecord);

        // Actualizar un registro de vacunacion existente
        Task UpdateAsync(VaccinationRecord VaccinationRecord);

        // Eliminar  por Id
        Task DeleteAsync(Guid id);

    }

