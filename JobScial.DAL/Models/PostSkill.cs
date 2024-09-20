using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class PostSkill
{
    public Guid PostId { get; set; }

    public int? SkillCategoryId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual SkillCategory? SkillCategory { get; set; }
}
