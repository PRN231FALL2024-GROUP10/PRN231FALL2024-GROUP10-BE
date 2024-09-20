using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Follow
{
    public Guid FollowId { get; set; }

    public int FollowType { get; set; }

    public Guid AccountId { get; set; }

    public DateTime? FollowedOn { get; set; }

    public virtual Post FollowNavigation { get; set; } = null!;
}
