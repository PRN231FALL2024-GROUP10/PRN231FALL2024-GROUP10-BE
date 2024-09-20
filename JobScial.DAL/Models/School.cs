using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class School
{
    public int SchoolId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AccountEducation> AccountEducations { get; set; } = new List<AccountEducation>();
}
