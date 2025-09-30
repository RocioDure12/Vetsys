using System;
using Vetsys.API.Modules.Pets;

namespace Vetsys.API.Modules.Customers
{
    public class Customer
    {
        // Propiedades obligatorias
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }

        public  string? Email { get; set; }

        public  string? TelegramId { get; set; }

        public ICollection<Pet> Pets { get; set; } = [];


    }
}
