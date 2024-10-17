using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobScial.DAL.Models;

public partial class SkillCategory
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int SkillCategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AccountSkill> AccountSkills { get; set; } = new List<AccountSkill>();
    [JsonIgnore]

    public virtual ICollection<PostSkill> PostSkills { get; set; } = new List<PostSkill>();
}
