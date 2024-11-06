using System;
using System.Collections.Generic;

namespace JobScial.DAL.Models;

public partial class Connection
{
    public int AccountId { get; set; }

    public int FollowedAccountId { get; set; }

    public DateTime? CreatedOn { get; set; }
}
