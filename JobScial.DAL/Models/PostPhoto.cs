using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobScial.DAL.Models;

public partial class PostPhoto
{
    public int PostId { get; set; }

    public int Index { get; set; }

    public string? Link { get; set; }

    public string? Caption { get; set; }
    [JsonIgnore]

    public virtual Post Post { get; set; } = null!;
}
