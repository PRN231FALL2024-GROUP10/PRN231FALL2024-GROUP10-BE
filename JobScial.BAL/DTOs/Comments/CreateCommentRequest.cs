using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Comments
{
    public class CreateCommentRequest
    {
        public string? Content { get; set; }

        public int? PostId { get; set; }
        public int? Status { get; set; }
    }
}
