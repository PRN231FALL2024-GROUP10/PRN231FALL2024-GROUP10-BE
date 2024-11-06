using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Group
{
    public class AddGroupRequest
    {
        public int? CompanyId { get; set; }

        public string? GroupName { get; set; }

        public bool? IsVerified { get; set; }

        public int? Status { get; set; }

    }
}
