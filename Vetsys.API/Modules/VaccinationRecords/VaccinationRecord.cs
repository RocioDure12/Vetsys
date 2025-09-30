using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vetsys.API.Modules.Pets;
using Vetsys.API.Modules.VaccinesTypes;

namespace Vetsys.API.Modules.VaccinationRecords
{
    public class VaccinationRecord
    {
        [Key]
        public required Guid Id { get; set; }  // Identidad única del registro

        [Required]
        public required Guid PetId { get; set; }  // FK hacia Pet

        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }  // Relación con la mascota


        [Required]
        public required Guid VaccineTypeId { get; set; }  // FK hacia VaccineType

        [ForeignKey(nameof(VaccineTypeId))]
        public  VaccineType ? VaccineType { get; set; }  // Relación con tipo de vacuna


        public required DateTime DateAdministered { get; set; }  // Fecha aplicacion

       
        public required DateTime ExpirationDate { get; set; }  // Fecha de vencimiento (vacunas proximas a vencer)

       
    }
}
