using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Movements
{
    public class DeleteMovementByIdHandler
    {
        private readonly IRepository<SupplierEntity> _repository;

        public DeleteMovementByIdHandler(IRepository<SupplierEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(int id)
        {
            var movement = await _repository.GetByIdAsync(id);
            if (movement == null) throw new NullReferenceException("That movement does not exist");

            await _repository.DeleteAsync(id);
        }
    }
}
