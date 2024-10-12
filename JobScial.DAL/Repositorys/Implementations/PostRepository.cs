using JobScial.DAL.Infrastructures;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobScial.DAL.Models;
using Microsoft.AspNetCore.Http;
using BMOS.BAL.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using GenZStyleAPP.BAL.Errors;
using Azure;
using JobScial.BAL.DTOs.Accounts;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class PostRepository : IPostRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthenticationRepository> _logger;

        public PostRepository(IUnitOfWork unitOfWork, ILogger<AuthenticationRepository> logger, IConfiguration configuration)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            //this._mapper = mapper;
            this._logger = logger;
            this._config = configuration;
        }

        public async Task<CommonResponse> AddPostAsync(CreatePostRequest post, HttpContext httpContext )
        {
            string loginSuccessMsg = _config["ResponseMessages:AuthenticationMsg:UnauthenticationMsg"];
            string CreatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:CreatePostSuccessedMsg"];
            string NotCreateSuccessMsg = _config["ResponseMessages:RolePermissionMsg:NotCreateSuccessMsg"];
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);

                Post post1 = new Post
                { 
                    Content = post.Content,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = accountStaff.AccountId,
                    HasPhoto  = post.HasPhoto,

                };

                await _unitOfWork.PostDAO.AddNewPost(post1);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = post1;
                commonResponse.Status = 200;
                commonResponse.Message = CreatePostSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 405;
            }
            return commonResponse;

        }

        public async Task<CommonResponse> UpdatePostAsync(CreatePostRequest post, HttpContext httpContext , int id)
        {
            string loginSuccessMsg = _config["ResponseMessages:AuthenticationMsg:UnauthenticationMsg"];
            string CreatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:CreatePostSuccessedMsg"];
            string NotCreateSuccessMsg = _config["ResponseMessages:RolePermissionMsg:NotCreateSuccessMsg"];
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);

                Post post1 = new Post
                {
                    Content = post.Content,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = accountStaff.AccountId,
                    HasPhoto = post.HasPhoto,

                };

                await _unitOfWork.PostDAO.UpdatePost(post1);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = post1;
                commonResponse.Status = 200;
                commonResponse.Message = CreatePostSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 405;
            }
            return commonResponse;
        }

        public async Task DeletePostAsync(int postId)
        {
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllPostsAsync(HttpContext httpContext)
        {
            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);
                // Lấy tất cả bài post
                List<Post> allPosts = await _unitOfWork.PostDAO.GetPosts();
                return allPosts;
            }
            catch (Exception ex)
            {
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
            }
        }


    }
}
