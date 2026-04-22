using Domain;
using Domain.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Users
{
    public class CreateUserHandler
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IRepository<UserEntity> repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(UserEntity entity)
        {
            var user = await _repository.GetByIdAsync(entity.Id);
            if (user != null) throw new InvalidOperationException("That user already exists");

            if (string.IsNullOrWhiteSpace(entity.Password)) throw new ArgumentException("Password must not be null or blank", nameof(entity.Password));
            if (entity.Password.Length < 8 || entity.Password.Length > 15) throw new ArgumentException("Password must be between 8 and 15 characters long", nameof(entity.Password));

            string hashedPassword = _passwordHasher.HashPassword(entity.Password);
            UserEntity newUser = new UserEntity(entity.Id, entity.Name, entity.Email, hashedPassword);
            await _repository.AddAsync(newUser);
        }
    }
}
