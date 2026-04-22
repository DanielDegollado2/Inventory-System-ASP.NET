using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products
{
    public class GetAllProductsHandler
    {
        private readonly IRepository<ProductEntity> _repository;

        public GetAllProductsHandler(IRepository<ProductEntity> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductEntity>> Handle()
        {
            IEnumerable<ProductEntity> products = await _repository.GetAllAsync();
            if (!products.Any()) throw new KeyNotFoundException("That product does not exist");
            return products;
        }
    }
}
