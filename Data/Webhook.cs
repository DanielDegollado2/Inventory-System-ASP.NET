using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Webhook
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int SupplierId { get; set; }
        public string Event { get; set; }
        public bool IsActive { get; set; }
        public Supplier Supplier { get; set; }
    }
}
