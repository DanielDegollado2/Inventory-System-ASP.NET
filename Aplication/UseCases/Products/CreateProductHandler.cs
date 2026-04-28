using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Products
{
    public class CreateProductHandler
    {
        private readonly IRepository<ProductEntity> _repository;

        public CreateProductHandler(IRepository<ProductEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProductEntity entity)
        {
            await _repository.AddAsync(entity);
        }
    }
}
