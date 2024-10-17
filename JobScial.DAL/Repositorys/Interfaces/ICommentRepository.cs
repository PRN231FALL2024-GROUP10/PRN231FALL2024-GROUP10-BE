using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Comments;
using JobScial.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Interfaces
{
    public interface ICommentRepository
    {
        
        Task<CommonResponse> AddCommentAsync(CreateCommentRequest comment,HttpContext httpContext);


        Task UpdateCommentAsync(Comment comment);


        Task<CommonResponse> DeleteCommentAsync(int commentId);


        Task<Comment> GetCommentByIdAsync(int commentId);


        Task<List<Comment>> GetAllCommentsAsync();
    }
}
