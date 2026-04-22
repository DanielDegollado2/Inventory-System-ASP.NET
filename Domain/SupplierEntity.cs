using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SupplierEntity
    {
        public SupplierEntity(int id, string name, string? phoneNumber, string? email) 
        { 
                if (id < 0) throw new ArgumentOutOfRangeException("Id must be greater than 0");
                if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public SupplierEntity() { }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; set; }
    }
}
