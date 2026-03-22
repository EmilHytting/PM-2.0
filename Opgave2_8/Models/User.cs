using System;
using System.Collections.Generic;

namespace Opgave2_8.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
