using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IWebhookRepository : IRepository<WebhookEntity>
    {
        Task<IEnumerable<WebhookEntity>> GetActiveBySupplierAndEventAsync(int supplierId, string eventType);
    }
}
