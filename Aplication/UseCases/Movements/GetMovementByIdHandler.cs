using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Movements
{
    public class GetMovementByIdHandler
    {
        private readonly IRepository<MovementEntity> _repository;

        public GetMovementByIdHandler(IRepository<MovementEntity> repository)
        {
            _repository = repository;
        }

        public async Task<MovementEntity> Handle(int id)
        {
            var movement = await _repository.GetByIdAsync(id);
            if (movement == null) throw new NullReferenceException("That movement does not exist");
            return movement;
        }
    }
}
