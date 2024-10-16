using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class JobTitle
{
    [Key]
    public int JobId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
