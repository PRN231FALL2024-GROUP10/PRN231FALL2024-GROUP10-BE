using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class AccountEducation
{
    public Guid AccountId { get; set; }

    public int SchoolId { get; set; }

    public int YearStart { get; set; }

    public int? Timespan { get; set; }

    public int? TimespanUnit { get; set; }

    public string? Description { get; set; }

    public virtual School School { get; set; } = null!;
}
