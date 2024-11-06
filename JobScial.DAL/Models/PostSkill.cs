using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobScial.DAL.Models;

public partial class PostSkill
{
    public int PostId { get; set; }

    public int? SkillCategoryId { get; set; }
    public int? YearRequirement { get; set; }

    public string? SkillLevelRequirement { get; set; }
    [JsonIgnore]

    public virtual Post Post { get; set; } = null!;

    public virtual SkillCategory? SkillCategory { get; set; }
}
