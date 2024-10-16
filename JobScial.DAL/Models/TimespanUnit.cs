using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class TimespanUnit
{
    [Key]
    public int TimespanUnitId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AccountExperience> AccountExperiences { get; set; } = new List<AccountExperience>();
}
