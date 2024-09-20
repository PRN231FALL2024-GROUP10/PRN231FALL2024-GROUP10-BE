using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class GroupMember
{
    public Guid GroupId { get; set; }

    public Guid AccountId { get; set; }

    public int? GroupRoleId { get; set; }

    public DateTime? JoinedOn { get; set; }

    public int? Status { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual GroupRole? GroupRole { get; set; }
}
