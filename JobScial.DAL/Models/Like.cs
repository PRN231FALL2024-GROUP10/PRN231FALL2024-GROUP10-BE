using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobScial.DAL.Models;

public partial class Like
{
    public int PostId { get; set; }

    public int AccountId { get; set; }
    [JsonIgnore]

    public virtual Post Post { get; set; } = null!;
}
