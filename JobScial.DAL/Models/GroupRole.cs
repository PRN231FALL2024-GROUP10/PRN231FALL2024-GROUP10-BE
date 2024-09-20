using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class GroupRole
{
    public int GroupRoleId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
}
