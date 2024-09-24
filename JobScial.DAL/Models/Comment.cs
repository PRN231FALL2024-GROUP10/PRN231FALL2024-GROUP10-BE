using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? AccountId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Status { get; set; }

    public virtual Post? Post { get; set; }
}
