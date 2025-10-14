using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vetsys.Tests.Modules.Shared.Models
{
    public record Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required DateOnly BirthDate { get; set; }
     
    }
}
