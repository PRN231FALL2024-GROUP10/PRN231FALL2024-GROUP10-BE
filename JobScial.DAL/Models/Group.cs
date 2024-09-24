using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public int? CompanyId { get; set; }

    public string? GroupName { get; set; }

    public bool? IsVerified { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Status { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
