using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
