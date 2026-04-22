using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Suppliers
{
    public class UpdateSupplierHandler
    {
        private readonly IRepository<SupplierEntity> _repository;

        public UpdateSupplierHandler(IRepository<SupplierEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(SupplierEntity entity)
        {
            var supplier = await _repository.GetByIdAsync(entity.Id);
            if (supplier == null) throw new NullReferenceException("That supplier does not exist");

            await _repository.UpdateAsync(entity);
        }
    }
}
