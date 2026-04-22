using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class UserDto
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }

        public UserDto(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
