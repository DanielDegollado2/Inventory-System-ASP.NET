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
            var product = await _repository.GetByIdAsync(entity.Id);
            if (product != null) throw new InvalidOperationException("That product already exists");
            await _repository.AddAsync(entity);
        }
    }
}
