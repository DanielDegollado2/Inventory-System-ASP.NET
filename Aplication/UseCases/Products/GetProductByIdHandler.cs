using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Products
{
    public  class GetProductByIdHandler
    {
        private readonly IRepository<ProductEntity> _repository;

        public GetProductByIdHandler(IRepository<ProductEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ProductEntity> Handle(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
