using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class AccountSkill
{
    public Guid AccountId { get; set; }

    public int SkillCategoryId { get; set; }

    public int? SkillLevel { get; set; }

    public int? Timespan { get; set; }

    public int? TimespanUnit { get; set; }

    public string? Description { get; set; }

    public virtual SkillCategory SkillCategory { get; set; } = null!;
}
