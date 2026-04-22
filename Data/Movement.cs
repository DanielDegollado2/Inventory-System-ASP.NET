using System;
using System.Collections.Generic;

namespace Data;

public partial class Movement
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public DateTime? Date { get; set; }

    public string Type { get; set; } = null!;

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
