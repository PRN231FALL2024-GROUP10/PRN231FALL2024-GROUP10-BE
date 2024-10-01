using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobScial.DAL.Models;

public partial class AccountCertificate
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int AccountId { get; set; }

    public int Index { get; set; }

    public string? Link { get; set; }
}
