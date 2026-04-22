using Aplication.DTOs;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Users
{
    public class UpdateUserHandler
    {
        private readonly IRepository<UserEntity> _repository;

        public UpdateUserHandler(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UserEntity entity)
        {
            var user = await _repository.GetByIdAsync(entity.Id);
            if (user == null) throw new NullReferenceException("That user does not exist");
            await _repository.UpdateAsync(user);
        }
    }
}
