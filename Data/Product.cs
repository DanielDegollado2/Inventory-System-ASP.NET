using System;
using System.Collections.Generic;

namespace Data;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int Stock { get; set; }

    public int MinimumStock { get; set; }

    public int SupplierId { get; set; }

    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();

    public virtual Supplier Supplier { get; set; } = null!;
}
