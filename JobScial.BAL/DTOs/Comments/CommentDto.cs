using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Comments
{
    public class CommentDto
    {
        [Key]
        public int CommentId { get; set; }

        public int? PostId { get; set; }

        public virtual Account? Account { get; set; }

        public string? Content { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
