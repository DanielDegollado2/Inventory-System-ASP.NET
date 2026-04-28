using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Suppliers
{
    public class GetAllSuppliersHandler
    {
        private readonly IRepository<SupplierEntity> _repository;

        public GetAllSuppliersHandler(IRepository<SupplierEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SupplierEntity>> Handle()
        {
            IEnumerable<SupplierEntity> suppliers = await _repository.GetAllAsync();
            if (!suppliers.Any()) throw new KeyNotFoundException("No movements found");
            return suppliers;
        }
    }
}
