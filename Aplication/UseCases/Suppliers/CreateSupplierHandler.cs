using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Suppliers
{
    public class CreateSupplierHandler
    {
        private readonly IRepository<SupplierEntity> _repository;

        public CreateSupplierHandler(IRepository<SupplierEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(SupplierEntity entity)
        {
            await _repository.AddAsync(entity);
        }
    }
}
