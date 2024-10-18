using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Posts
{
    public class UpdatePostRequest
    {
        public string? Content { get; set; }
        public bool? HasPhoto { get; set; }

        public List<IFormFile>? Link { get; set; } // Đảm bảo kiểu dữ liệu này

        public List<string>? Skills { get; set; }   
        public string? Category { get; set; }     
        public string? JobTitle { get; set; }   
    }
}
