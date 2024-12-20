﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class AccountEducation
{
    [Key]
    public int AccountId { get; set; }
    [Key]
    public int SchoolId { get; set; }
    [Key]
    public int YearStart { get; set; }

    public int? Timespan { get; set; }

    public int? TimespanUnit { get; set; }

    public string? Description { get; set; }

    public virtual School School { get; set; } = null!;
}
