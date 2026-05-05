using Application.Abstractions;
using Domain;

namespace Backend.WebSockets
{
    public class WebSocketStockNotifier : IStockNotifier
    {
        private readonly WebSocketConnectionManager _manager;

        public WebSocketStockNotifier(WebSocketConnectionManager manager)
        {
            _manager = manager;
        }

        public async Task NotifyLowStock(ProductEntity product)
        {
            await _manager.SendMessage($"Low stock alert: {product.Name} has {product.Stock} units remaining, minimum is {product.MinimumStock}");
        }
    }
}
