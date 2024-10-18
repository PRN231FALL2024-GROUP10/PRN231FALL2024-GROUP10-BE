using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Posts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IlikeRepository 
    {
        public Task<CommonResponse> AddLikeAsync(CreateLike like, HttpContext httpContext);

    }
}
