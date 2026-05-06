using Domain;

namespace Backend.WebHooks
{
    public class WebhookNotifier
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebhookNotifier(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task NotifyLowStock(ProductEntity product)
        {
            var client = _httpClientFactory.CreateClient();
        }
    }
}
