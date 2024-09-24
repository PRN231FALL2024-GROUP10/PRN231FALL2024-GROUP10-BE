using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public int? Gender { get; set; }

    public int? Role { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Image { get; set; }

    public string? FullName { get; set; }

    public string? FullNameSearch { get; set; }

    public DateTime? DoB { get; set; }
}
