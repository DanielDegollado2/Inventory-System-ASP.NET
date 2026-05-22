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
using System.IdentityModel.Tokens.Jwt;
using Application.Abstractions;


namespace Application.UseCases.Users
{
    public class LoginHandler
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginHandler(IUserRepository repository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Handle(string userName, string password)
        {
            var user = await _repository.GetUserByUsername(userName);
            if (user == null) throw new InvalidOperationException("That user does not exists");

            if(_passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
            {
                var token = _tokenGenerator.GenerateToken(user);
                return token;
            }
            else(_passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidOperationException("Invalid password");
            }
        }
    }
}
