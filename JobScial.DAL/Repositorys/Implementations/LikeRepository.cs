using BMOS.BAL.Helpers;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Comments;
using JobScial.BAL.DTOs.Posts;
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
    public class LikeRepository : IlikeRepository
    {
        private UnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public LikeRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _config = configuration;
        }

        public async Task<CommonResponse> AddLikeAsync(CreateLike like, HttpContext httpContext)
        {
            string CreateLikeSuccessedMsg = _config["ResponseMessages:CommonMsg:CreateLikeSuccessedMsg"];

            CommonResponse commonResponse = new CommonResponse();
            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);
                var post = await _unitOfWork.PostDAO.GetPostById(like.PostId);

                if (post == null)
                {
                    throw new Exception("Post not found.");
                }
                Like like1 = new Like
                {
                    AccountId = accountStaff.AccountId,
                    PostId = like.PostId,
                    Post = post,
                };

                await _unitOfWork.likeDAO.AddNewLike(like1);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = like1;
                commonResponse.Status = 200;
                commonResponse.Message = CreateLikeSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 405;
            }
            return commonResponse;

        }
    }
}
