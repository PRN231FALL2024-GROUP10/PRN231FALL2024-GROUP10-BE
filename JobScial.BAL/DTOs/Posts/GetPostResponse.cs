using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Posts
{
    public class GetPostResponse
    {
        public List<string>? Photo { get; set; }

        public string? Content { get; set; }
        public List<string>? Skill { get; set; }
        public string? jobTitle { get; set; }
        public string? Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
