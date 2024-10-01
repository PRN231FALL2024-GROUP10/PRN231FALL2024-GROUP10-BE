using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class Group
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

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
