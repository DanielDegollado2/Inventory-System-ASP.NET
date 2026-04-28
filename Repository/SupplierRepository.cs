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
    public class SupplierRepository : IRepository<SupplierEntity>
    {
        private InventoryContext _context;

        public SupplierRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SupplierEntity entity)
        {
            Supplier supplier = MapToModel(entity);
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rows = await _context.Suppliers.Where(s => s.Id == id).ExecuteDeleteAsync();
            if (rows == 0) throw new KeyNotFoundException($"Supplier with id {id} was not found.");
        }

        public async Task<IEnumerable<SupplierEntity>> GetAllAsync()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return suppliers.Select(MapToEntity);
        }

        public async Task<SupplierEntity> GetByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(p => p.Id == id);
            if (supplier == null) { throw new KeyNotFoundException($"Supplier with id {id} was not found."); }

            return MapToEntity(supplier);
        }

        public async Task UpdateAsync(SupplierEntity entity)
        {
            var supplier = await _context.Suppliers.FindAsync(entity.Id);
            if (supplier == null) throw new KeyNotFoundException("Supplier not found");
            
            supplier.Name = entity.Name;
            supplier.PhoneNumber = entity.PhoneNumber;
            supplier.Email = entity.Email;

            await _context.SaveChangesAsync();
        }

        #region Mappers
        private SupplierEntity MapToEntity(Supplier model)
        {
            return new SupplierEntity(model.Id, model.Name, model.PhoneNumber, model.Email);
        }

        private Supplier MapToModel(SupplierEntity entity)
        {
            return new Supplier
            {
                Id = entity.Id,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
        }
        #endregion
    }
}
