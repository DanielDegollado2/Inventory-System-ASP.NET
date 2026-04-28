using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
        public int SupplierId { get; set; }

        public ProductEntity(int id, string name, string code, int stock, int minimumStock, int supplierId)
        {
            code = code.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            if (code.Length != 8 || !code.All(char.IsLetterOrDigit))
                throw new ArgumentException("Code must be 8 alphanumeric characters", nameof(code));
            if (minimumStock < 1)
                throw new ArgumentException("Minimum stock must be greater than 0", nameof(minimumStock));
            if (stock < minimumStock)
                throw new ArgumentException("Initial stock cannot be below minimum", nameof(stock));

            Id = id;
            Name = name;
            Code = code;
            Stock = stock;
            MinimumStock = minimumStock;
            SupplierId = supplierId;
        }

        public ProductEntity() { }

        public void RegisterEntry(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            Stock += quantity;
        }

        public void RegisterExit(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            if (quantity > Stock)
                throw new InvalidOperationException("Not enough stock available");
            Stock -= quantity;
        }
    }
}
