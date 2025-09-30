using Vetsys.API.Modules.Customers;

namespace Vetsys.API.Modules.Pets
{
    public class Pet
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Customer? Customer { get; set; }
    }
}
    