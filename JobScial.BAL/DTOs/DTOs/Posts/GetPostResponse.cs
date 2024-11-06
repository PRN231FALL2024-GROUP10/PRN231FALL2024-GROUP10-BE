using JobScial.BAL.DTOs.Comments;
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
        public int PostID { get; set; }
        public List<string>? Photo { get; set; }

        public string? Content { get; set; }
        public List<string>? Skill { get; set; }
        public string? jobTitle { get; set; }
        public string? Category { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Account Account { get; set; }
        public int likeCount { get; set; }
        public bool isLiked { get; set; }
        public int? privacyLevel { get; set; }
        public virtual ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
