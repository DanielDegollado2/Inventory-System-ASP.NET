using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MovementEntity
    {
        public MovementEntity(int id, int quantity, DateTime? date, string type, int productId)
        {
            if (id <= 0) throw new ArgumentException("Id must be greater than 0", nameof(id));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
            if (!type.Equals("Extraction") || !type.Equals("Addition")) throw new ArgumentException("Type must be either Extraction or Addition", nameof(quantity));

            Id = id;
            Quantity = quantity;
            Date = date;
            Type = type;
            ProductId = productId;
        }

        public MovementEntity() { }

        public int Id { get;  set; }
        public int Quantity { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public int ProductId { get; set; }
    }
}
