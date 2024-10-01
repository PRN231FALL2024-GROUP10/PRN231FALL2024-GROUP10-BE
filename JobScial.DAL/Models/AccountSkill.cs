using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class AccountSkill
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int AccountId { get; set; }

    public int SkillCategoryId { get; set; }

    public int? SkillLevel { get; set; }

    public int? Timespan { get; set; }

    public int? TimespanUnit { get; set; }

    public string? Description { get; set; }

    public virtual SkillCategory SkillCategory { get; set; } = null!;
}
