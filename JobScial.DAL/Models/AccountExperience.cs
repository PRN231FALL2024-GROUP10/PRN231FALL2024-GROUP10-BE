using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class AccountExperience
{
    public Guid AccountId { get; set; }

    public int CompanyId { get; set; }

    public int YearStart { get; set; }

    public string? JobTitle { get; set; }

    public int? Timespan { get; set; }

    public int? TimespanUnit { get; set; }

    public string? Description { get; set; }

    public virtual TimespanUnit? TimespanUnitNavigation { get; set; }
}
