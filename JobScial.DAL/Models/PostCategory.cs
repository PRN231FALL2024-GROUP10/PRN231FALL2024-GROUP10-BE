using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class PostCategory
{
    public int PostCategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
