using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Posts
{
    public class PostsPhotoRequest
    {
        public string? Link { get; set; }

        public string? Caption { get; set; }
    }
}
