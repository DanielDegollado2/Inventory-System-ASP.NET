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

        public UserEntity(int id, string userName, string password) 
        {
            if (id <= 0) throw new ArgumentException("Id must be greater than 0", nameof(id));
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Name must not be null or blank", nameof (userName));
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Email must not be null or blank", nameof(userName));
            if (!EmailRegex.IsMatch(userName)) throw new ArgumentException("Invalid Email format", nameof(userName));

            Id = id;
            Username = userName;
            Password = password;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
