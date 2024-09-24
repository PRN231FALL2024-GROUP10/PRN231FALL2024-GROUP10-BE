using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class PostPhoto
{
    public int PostId { get; set; }

    public int Index { get; set; }

    public string? Link { get; set; }

    public string? Caption { get; set; }

    public virtual Post Post { get; set; } = null!;
}
