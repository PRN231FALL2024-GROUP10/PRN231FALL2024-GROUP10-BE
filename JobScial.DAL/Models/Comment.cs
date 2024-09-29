using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class Comment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? AccountId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Status { get; set; }

    public virtual Post? Post { get; set; }
}
