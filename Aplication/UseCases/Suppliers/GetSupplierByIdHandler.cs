using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Suppliers
{
    public class GetSupplierByIdHandler
    {
        private readonly IRepository<SupplierEntity> _repository;

        public GetSupplierByIdHandler(IRepository<SupplierEntity> repository)
        {
            _repository = repository;
        }

        public async Task<SupplierEntity> Handle(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new NullReferenceException("That supplier does not exist");
            return product;
        }
    }
}
