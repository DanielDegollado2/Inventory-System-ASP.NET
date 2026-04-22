using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Movements
{
    public class CreateMovementHandler
    {
        private readonly IRepository<MovementEntity> _repository;

        public CreateMovementHandler(IRepository<MovementEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(MovementEntity entity)
        {
            var movement = await _repository.GetByIdAsync(entity.Id);
            if (movement != null) throw new InvalidOperationException("That movement already exists");
            await _repository.AddAsync(entity);
        }
    }
}
