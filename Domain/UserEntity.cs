using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class UserEntity
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public UserEntity(int id, string userName, string email, string password) 
        {
            if (id <= 0) throw new ArgumentException("Id must be greater than 0", nameof(id));
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Name must not be null or blank", nameof (userName));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email must not be null or blank", nameof(email));
            if (!EmailRegex.IsMatch(email)) throw new ArgumentException("Invalid Email format", nameof(email));

            Id = id;
            Username = userName;
            Email = email;
            Password = password;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
