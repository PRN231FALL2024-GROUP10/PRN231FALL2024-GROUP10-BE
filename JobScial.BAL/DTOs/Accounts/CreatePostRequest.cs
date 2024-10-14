using JobScial.BAL.DTOs.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Accounts
{
    public class CreatePostRequest
    {
        public string? Content { get; set; }
        public bool? HasPhoto { get; set; }

        public List<IFormFile>? Link { get; set; } // Đảm bảo kiểu dữ liệu này

    }

    public class PostPhotoRequest
    {
        [FromForm]

        public IFormFile? Link { get; set; } // Đảm bảo kiểu dữ liệu này
        [FromForm]

        public string Caption { get; set; }
    }
}
