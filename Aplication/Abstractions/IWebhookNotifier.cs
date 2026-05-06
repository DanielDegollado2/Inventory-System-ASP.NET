using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IWebhookNotifier
    {
        Task NotifyLowStock(ProductEntity product);
    }
}
