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
        Task<CommonResponse> AddPostAsync(CreatePostRequest post, HttpContext httpContext);

        Task<CommonResponse> AddPostPhotoAsync(int postId, List<IFormFile>? links, HttpContext httpContext, FireBaseImage fireBaseImage);

        // Sửa bài viết
        Task<CommonResponse> UpdatePostAsync(CreatePostRequest post, HttpContext httpContext, int id);
        Task<CommonResponse> UpdatePostPhotoAsync(CreatePostRequest post, HttpContext httpContext, int id, FireBaseImage fireBaseImage);

        // Xóa bài viết theo id
        Task DeletePostAsync(int postId);

        // Lấy bài viết theo id
        Task<Post> GetPostByIdAsync(int postId);

        Task<List<GetPostResponse>> GetPostByAccountIdAsync(int accountId, HttpContext httpContext);
        Task<List<GetPostResponse>> GetPostByAccountLikeIdAsync(int accountId, HttpContext httpContext);
        Task<List<GetPostResponse>> GetPostByAccountCommentIdAsync(int accountId, HttpContext httpContext);

        // Lấy tất cả bài viết
        Task<List<GetPostResponse>> GetAllPostsAsync(HttpContext httpContext);

        Task<List<GetPostResponse>> GetAllPostsByUser(HttpContext httpContext);

        Task<List<Post>> GetPostByUserName(string username);

        Task<CommonResponse> LikePost(int id, HttpContext httpContext);
    }
}
