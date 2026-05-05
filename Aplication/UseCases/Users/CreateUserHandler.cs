using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Users
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public CreateUserHandler(IUserRepository repository, IPasswordHasher<UserEntity> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(UserEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Password)) throw new ArgumentException("Password must not be null or blank", nameof(entity.Password));
            if (entity.Password.Length < 8 || entity.Password.Length > 15) throw new ArgumentException("Password must be between 8 and 15 characters long", nameof(entity.Password));
            
            var user = await _repository.GetUserByUsername(entity.Username);
            if (user != null) throw new InvalidOperationException("That user already exists");

            entity.Password = _passwordHasher.HashPassword(entity, entity.Password);
            await _repository.AddAsync(entity);
        }
    }
}
