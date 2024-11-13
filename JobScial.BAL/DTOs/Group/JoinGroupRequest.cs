using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Group
{
    public class JoinGroupRequest
    {
        public int GroupId { get; set; }

        public int AccountId { get; set; }

        public int? GroupRoleId { get; set; }

        public DateTime? JoinedOn { get; set; }

        public int? Status { get; set; }
    }
}
