using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Post
{
    public Guid PostId { get; set; }

    public string? Content { get; set; }

    public int? PostCategoryId { get; set; }

    public Guid? GroupId { get; set; }

    public int? JobId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public bool? HasPhoto { get; set; }

    public int? PrivateLevel { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Follow> Follows { get; set; } = new List<Follow>();

    public virtual Group? Group { get; set; }

    public virtual JobTitle? Job { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual PostCategory? PostCategory { get; set; }

    public virtual ICollection<PostPhoto> PostPhotos { get; set; } = new List<PostPhoto>();

    public virtual PostSkill? PostSkill { get; set; }
}
