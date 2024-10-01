using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class GroupMember
{
    public int GroupId { get; set; }

    public int AccountId { get; set; }

    public int? GroupRoleId { get; set; }

    public DateTime? JoinedOn { get; set; }

    public int? Status { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual GroupRole? GroupRole { get; set; }
}
