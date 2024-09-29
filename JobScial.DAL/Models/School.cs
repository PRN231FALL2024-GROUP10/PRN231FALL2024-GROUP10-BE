using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class School
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int SchoolId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AccountEducation> AccountEducations { get; set; } = new List<AccountEducation>();
}
