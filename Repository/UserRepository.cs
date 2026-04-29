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
    public class UserRepository : IUserRepository
    {

        private readonly InventoryContext _context;
        public UserRepository(InventoryContext context) 
        { 
            _context = context;
        }

        public Task AddAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetUserByUsername(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(userName));
            if (user == null) return null;
            return MapToEntity(user);
        }

        public Task UpdateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        #region Mappers
        private UserEntity MapToEntity(User model)
        {
            return new UserEntity(model.Id, model.Username, model.Password);
        }

        private User MapToModel(UserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                Username = entity.Username,
                Password = entity.Password
            };
        }

        
        #endregion
    }
}
