using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Like
{
    public Guid PostId { get; set; }

    public Guid AccountId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
