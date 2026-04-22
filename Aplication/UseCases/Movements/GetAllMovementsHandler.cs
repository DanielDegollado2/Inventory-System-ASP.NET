using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Movements
{
    public class GetAllMovementsHandler
    {
        private readonly IRepository<MovementEntity> _repository;

        public GetAllMovementsHandler(IRepository<MovementEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MovementEntity>> Handle()
        {
            IEnumerable<MovementEntity> movements = await _repository.GetAllAsync();
            if (!movements.Any()) throw new KeyNotFoundException("No movements found");
            return movements;
        }
    }
}
