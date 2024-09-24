using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class AccountCertificate
{
    public int AccountId { get; set; }

    public int Index { get; set; }

    public string? Link { get; set; }
}
