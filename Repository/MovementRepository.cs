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
    public class MovementRepository : IRepository<MovementEntity>
    {
        private InventoryContext _context;
        public MovementRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MovementEntity entity)
        {
            Movement movement = MapToModel(entity);
            await _context.Movements.AddAsync(movement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rows = await _context.Movements.Where(s => s.Id == id).ExecuteDeleteAsync();
            if (rows == 0) throw new KeyNotFoundException($"Movement with id {id} was not found.");
        }

        public async Task<IEnumerable<MovementEntity>> GetAllAsync()
        {
            var movements = await _context.Movements.ToListAsync();
            return movements.Select(MapToEntity);
        }

        public async Task<MovementEntity> GetByIdAsync(int id)
        {
            var movement = await _context.Movements.FirstOrDefaultAsync(p => p.Id == id);
            if (movement == null) { throw new KeyNotFoundException(); }

            return MapToEntity(movement);
        }

        public async Task UpdateAsync(MovementEntity entity)
        {
            var movement = await _context.Movements.FindAsync(entity.Id);
            if (movement == null) throw new KeyNotFoundException("Movement not found");

            movement.Quantity = entity.Quantity;
            movement.Date = entity.Date;
            movement.Type = entity.Type;
            movement.ProductId = entity.ProductId;

            await _context.SaveChangesAsync();
        }

        #region Mappers
        private MovementEntity MapToEntity(Movement model)
        {
            return new MovementEntity(model.Id, model.Quantity, model.Date, model.Type, model.ProductId);
        }

        private Movement MapToModel(MovementEntity entity)
        {
            return new Movement
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                Date = entity.Date,
                Type = entity.Type,
                ProductId = entity.ProductId
            };
        }
        #endregion
    }
}
