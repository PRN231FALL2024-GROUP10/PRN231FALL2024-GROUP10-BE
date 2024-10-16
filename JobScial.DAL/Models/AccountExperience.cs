using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class AccountExperience
{
    [Key]
    public int AccountId { get; set; }
    [Key]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    [Key]
    public int YearStart { get; set; }

    public string? JobTitle { get; set; }

    public int? Timespan { get; set; }

    public int? TimespanUnit { get; set; }

    public string? Description { get; set; }

    public virtual TimespanUnit? TimespanUnitNavigation { get; set; }
}
