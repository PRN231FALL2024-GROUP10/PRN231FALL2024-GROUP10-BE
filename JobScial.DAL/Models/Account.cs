using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

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
