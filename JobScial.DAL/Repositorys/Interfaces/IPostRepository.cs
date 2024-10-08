using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Interfaces
{
     public interface  IPostRepository
    {
        // Thêm bài viết mới
        Task<CommonResponse> AddPostAsync(CreatePostRequest post, HttpContext httpContext);

        // Sửa bài viết
        Task<CommonResponse> UpdatePostAsync(CreatePostRequest post, HttpContext httpContext, int id);

        // Xóa bài viết theo id
        Task DeletePostAsync(int postId);

        // Lấy bài viết theo id
        Task<Post> GetPostByIdAsync(int postId);

        // Lấy tất cả bài viết
        Task<List<Post>> GetAllPostsAsync(HttpContext httpContext);
    }
}
