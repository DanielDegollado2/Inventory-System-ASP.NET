using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products
{
    public class DeleteProductByIdHandler
    {
        private readonly IRepository<ProductEntity> _repository;

        public DeleteProductByIdHandler(IRepository<ProductEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("That product does not exist");

            await _repository.DeleteAsync(id);
        }
    }
}
