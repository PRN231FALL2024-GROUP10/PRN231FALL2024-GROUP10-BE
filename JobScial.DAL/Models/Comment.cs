using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Comment
{
    public Guid CommentId { get; set; }

    public Guid? PostId { get; set; }

    public Guid? AccountId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Status { get; set; }

    public virtual Post? Post { get; set; }
}
