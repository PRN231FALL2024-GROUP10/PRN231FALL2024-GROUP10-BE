using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Posts
{
    public class CreateLike
    {
        public int PostId { get; set; }

        public int AccountId { get; set; }
    }
}
