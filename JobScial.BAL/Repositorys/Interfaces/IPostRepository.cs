using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.FireBase;
using JobScial.BAL.DTOs.Posts;
using JobScial.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
     public interface  IPostRepository
    {
        // Thêm bài viết mới
        Task<CommonResponse> AddPostAsync(CreatePostRequest post, FireBaseImage fireBaseImage, HttpContext httpContext);

        // Sửa bài viết
        Task<CommonResponse> UpdatePostAsync(UpdatePostRequest post, FireBaseImage fireBaseImage, HttpContext httpContext, int id);

        // Xóa bài viết theo id
        Task DeletePostAsync(int postId);

        // Lấy bài viết theo id
        Task<Post> GetPostByIdAsync(int postId);

        // Lấy tất cả bài viết
        Task<List<GetPostResponse>> GetAllPostsAsync(HttpContext httpContext);
        Task<List<GetPostResponse>> GetAllPostsByUser(HttpContext httpContext);

        Task<List<Post>> GetPostByUserName(string username);
    }
}
