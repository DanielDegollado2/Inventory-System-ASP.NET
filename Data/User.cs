using System;
using System.Collections.Generic;

namespace Data;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Role { get; set; }

    public string? Password { get; set; }
}
