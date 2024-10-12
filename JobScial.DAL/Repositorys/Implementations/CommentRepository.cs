using BMOS.BAL.Helpers;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Comments;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private UnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public CommentRepository(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _config = configuration;
        }
        public async Task<CommonResponse> AddCommentAsync(CreateCommentRequest comment ,HttpContext httpContext)
        {
            string CreateCommentSuccessedMsg = _config["ResponseMessages:CommonMsg:CreateCommentSuccessedMsg"];

            CommonResponse commonResponse = new CommonResponse();
            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);

                Comment comment1 = new Comment
                {
                    Content = comment.Content,
                    CreatedOn = DateTime.UtcNow,
                    PostId = comment.PostId,
                    AccountId = accountStaff.AccountId,
                    Status = comment.Status,    
                };

                await _unitOfWork.CommentDAO.AddNewComment(comment1);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = comment1;
                commonResponse.Status = 200;
                commonResponse.Message = CreateCommentSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 405;
            }
            return commonResponse;

        }

        public async Task DeleteCommentAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

    }
}
