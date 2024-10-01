using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Like
{
    public int PostId { get; set; }

    public int AccountId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
