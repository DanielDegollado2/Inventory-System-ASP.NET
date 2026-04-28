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
            await _repository.AddAsync(entity);
        }
    }
}
