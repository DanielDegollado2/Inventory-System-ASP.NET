using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class WebhookEntity
    {
        public WebhookEntity(int id, string url, int supplierId, string eve, bool isActive)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || uri.Scheme != Uri.UriSchemeHttps)
                throw new ArgumentException("Invalid HTTPS URL");
            if (!(eve.Equals("LOW_STOCK") || eve.Equals("PLACE_ORDER")))  
                throw new ArgumentException("This is not a valid event", nameof(eve)); 
        }

        public WebhookEntity() { }

        public int Id { get; set; } 
        public string Url { get; set; }
        public int SupplierId { get; set; }
        public string Event { get; set; }
        public bool IsActive { get; set; }
    }
}
