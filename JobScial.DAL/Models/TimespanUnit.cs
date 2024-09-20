using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class TimespanUnit
{
    public int TimespanUnitId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AccountExperience> AccountExperiences { get; set; } = new List<AccountExperience>();
}
