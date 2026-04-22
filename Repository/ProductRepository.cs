using Data;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IRepository<ProductEntity>
    {
        private InventoryContext _context;

        public ProductRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductEntity entity)
        {
            Product product = MapToModel(entity);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
           var products = await _context.Products.ToListAsync();
            return products.Select(MapToEntity);
        }

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) { throw new KeyNotFoundException(); }

            return MapToEntity(product);
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
            var product = await _context.Products.FindAsync(entity.Id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            product.Name = entity.Name;
            product.Code = entity.Code;
            product.Stock = entity.Stock;
            product.MinimumStock = entity.MinimumStock;
            product.SupplierId = entity.SupplierId;

            await _context.SaveChangesAsync();
        }

        #region Mappers
        private ProductEntity MapToEntity(Product model)
        {
            return new ProductEntity(model.Id, model.Name, model.Code, model.Stock , model.MinimumStock, model.SupplierId);
        }

        private Product MapToModel(ProductEntity entity)
        {
            return new Product
            {
                Name = entity.Name,
                Code = entity.Code,
                Stock = entity.Stock,
                MinimumStock = entity.MinimumStock,
            };
        }
        #endregion

    }
}
