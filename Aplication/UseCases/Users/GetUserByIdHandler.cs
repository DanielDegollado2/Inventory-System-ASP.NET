using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Users
{
    using Aplication.DTOs;

    public class GetUserByIdHandler
    {
        private readonly IRepository<UserEntity> _repository;

        public GetUserByIdHandler(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        /*
        public Task<UserDto> Handle(int id)
        {
            
            var user = await _repository.GetByIdAsync(id);
            if (user == null) throw new NullReferenceException("That user does not exist");
            return new UserDto(user.Id, user.Name, user.Email);
            
        }*/
        
    }
}
