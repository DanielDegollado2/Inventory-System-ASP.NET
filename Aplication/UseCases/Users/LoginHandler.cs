using Domain;
using Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users
{
    public class LoginHandler
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(string userName, string password)
        {
            var user = await _repository.GetUserByUsername(userName);
            if (user == null) throw new InvalidOperationException("That user does not exists");

            if(_passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
            {

            }
            else if(_passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Failed)
            {

            }
        }

        /*
        private string GenerateJwtToken(UserEntity user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim("name", user.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHere"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourapp",
                audience: "yourapp",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        */

    }
}
