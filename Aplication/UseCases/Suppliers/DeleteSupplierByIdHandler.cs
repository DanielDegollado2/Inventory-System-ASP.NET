using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Suppliers
{
    public class DeleteSupplierByIdHandler
    {
        private readonly IRepository<SupplierEntity> _repository;

        public DeleteSupplierByIdHandler(IRepository<SupplierEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(int id)
        {
            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null) throw new NullReferenceException("That supplier does not exist");

            await _repository.DeleteAsync(id);
        }
    }
}
