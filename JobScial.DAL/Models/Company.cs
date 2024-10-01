using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class Company
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
