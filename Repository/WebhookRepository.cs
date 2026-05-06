using Data;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WebhookRepository : IWebhookRepository
    {
        private readonly InventoryContext _context;
        public WebhookRepository(InventoryContext context)
        {
            _context = context;
        }
        public Task AddAsync(WebhookEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WebhookEntity>> GetActiveBySupplierAndEventAsync(int supplierId, string eventType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WebhookEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebhookEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WebhookEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
