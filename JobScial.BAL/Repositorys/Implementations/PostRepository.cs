﻿using JobScial.DAL.Infrastructures;
using JobScial.BAL.Repositorys.Interfaces;
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
using JobScial.BAL.DTOs.Posts;
using JobScial.BAL.DTOs.Comments;
using JobScial.BAL.DTOs.FireBase;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobScial.BAL.Repositorys.Implementations
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

        public async Task<CommonResponse> AddPostAsync(CreatePostRequest post, FireBaseImage fireBaseImage, HttpContext httpContext )
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
                    GroupId = post.GroupId,
                    PrivateLevel = post.PrivateLevel,

                };
                List<PostPhoto> postPhotosToSave = new List<PostPhoto>();
                await _unitOfWork.PostDAO.AddNewPost(post1);
                await _unitOfWork.CommitAsync(); // Lưu thay đổi để PostId được gán

                // Xử lý kỹ năng của bài đăng
                List<PostSkill> postSkillsToSave = new List<PostSkill>();
                if (post.Skills != null)
                {
                    foreach (var skill in post.Skills)
                    {
                        var skillCategory = await _unitOfWork.SkillCategoryDAO.GetSkillCategoryByName(skill);
                        if (skillCategory != null)
                        {
                            PostSkill postSkill = new PostSkill
                            {
                                PostId = post1.PostID,
                                SkillCategoryId = skillCategory.SkillCategoryId,
                            };
                            postSkillsToSave.Add(postSkill);
                        }
                    }
                }

                
                // Lưu danh sách PostSkill
                if (postSkillsToSave.Any())
                {
                    foreach (var postSkill in postSkillsToSave)
                    {
                         _unitOfWork.PostSkillDao.Add(postSkill);
                    }
                }

                JobTitle jobTitle = new JobTitle
                {
                    Name = post.JobTitle,
                };


                //PostCategory postCategory = new PostCategory
                //{
                //   Name = post.Category,
                //};
                // Xử lý nếu có ảnh
                if (post.HasPhoto == true && post.Link != null && post.Link.Any())
                {
                    int index = 0;
                    foreach (var imageFile in post.Link)
                    {
                        FileHelper.SetCredentials(fireBaseImage);
                        FileStream fileStream = FileHelper.ConvertFormFileToStream(imageFile);
                        Tuple<string, string> result = await FileHelper.UploadImage(fileStream, "Post");
                        // Lặp qua từng file trong danh sách file ảnh

                        // Lưu file và tạo đối tượng PostPhoto
                        var photo = new PostPhoto
                        {
                            PostId = post1.PostID,
                            Link = result.Item1,  // Lưu file và lấy đường dẫn
                            Caption = "", // Chú thích cho ảnh
                            Index = index++, // Chỉ số của ảnh
                            Post = post1             // Liên kết PostPhoto với Post

                            };

                            // Thêm ảnh vào danh sách PostPhotos của bài post
                            post1.PostPhotos.Add(photo);
                            postPhotosToSave.Add(photo);
                        
                    }
                }

                // Lưu danh sách ảnh sau
                if (postPhotosToSave.Any())
                {
                    foreach (var postPhoto in postPhotosToSave)
                    {
                        await _unitOfWork.PostPhotoDAO.AddNewPostPhoto(postPhoto);  // Lưu từng PostPhoto
                    }
                }
                post1.PostCategoryId = post.CategoryID;
                post1.Job = jobTitle;
                //await _unitOfWork.PostCategoryDA0.AddPostCategory(postCategory);
                await _unitOfWork.JobTitleDao.AddJobTitle(jobTitle);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = post1;
                commonResponse.Status = 200;
                commonResponse.Message = CreatePostSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 500; // Internal Server Error
            }
            return commonResponse;

        }
        // Phương thức lưu file và trả về đường dẫn
        private async Task<string> SaveFile(IFormFile file)
        {
            // Đường dẫn tới thư mục Uploads
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Tạo tên file duy nhất cho file tải lên
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Đường dẫn đầy đủ để lưu file
            var filePath = Path.Combine(uploadPath, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối hoặc đường dẫn đầy đủ
            return $"/Uploads/{fileName}";
        }

        public async Task<CommonResponse> UpdatePostAsync(UpdatePostRequest post, FireBaseImage fireBaseImage, HttpContext httpContext , int id)
        {
            string loginSuccessMsg = _config["ResponseMessages:AuthenticationMsg:UnauthenticationMsg"];
            string UpdatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:UpdatePostSuccessedMsg"];
            string NotUpdateSuccessMsg = _config["ResponseMessages:RolePermissionMsg:NotUpdateSuccessMsg"];
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);

                // Lấy bài viết cần cập nhật từ cơ sở dữ liệu
                var existingPost = await _unitOfWork.PostDAO.GetPostById(id);
                if (existingPost == null)
                {
                    commonResponse.Message = "Post not found.";
                    commonResponse.Status = 404; // Not Found
                    return commonResponse;
                }

                // Cập nhật thông tin bài viết
                existingPost.Content = post.Content;
                existingPost.HasPhoto = post.HasPhoto;
                

                await _unitOfWork.JobTitleDao.DeleteJobtile(existingPost.JobId);
                // ko cho cap nhap post category nha
                //await _unitOfWork.PostCategoryDA0.DeletePostCategory(existingPost.PostCategoryId);

                JobTitle jobTitle = new JobTitle
                {
                    Name = post.JobTitle,
                };


                PostCategory postCategory = new PostCategory
                {
                    Name = post.Category,
                };
                List<PostPhoto> postPhotosToSave = new List<PostPhoto>();

                // Xử lý nếu có ảnh được cập nhật
                if (post.HasPhoto == true && post.Link != null && post.Link.Any())
                {
                    int index = 0;

                    // Xóa các ảnh cũ nếu cần cập nhật
                    existingPost.PostPhotos.Clear();
                    // Tìm PostPhoto dựa trên ID
                    var existingPhoto = await _unitOfWork.PostPhotoDAO.GetAllById(id);

                    foreach (var photo in existingPhoto)  // Sử dụng ToList() để tránh lỗi khi thay đổi danh sách trong khi lặp
                    {
                        await _unitOfWork.PostPhotoDAO.DeleteFirstPostPhotoByPostIdAsync(photo.PostId);  // Gọi hàm xóa ảnh theo Id
                    }
                    foreach (var imageFile in post.Link)
                    {
                        FileHelper.SetCredentials(fireBaseImage);
                        FileStream fileStream = FileHelper.ConvertFormFileToStream(imageFile);
                        Tuple<string, string> result = await FileHelper.UploadImage(fileStream, "Post");

                        var photo = new PostPhoto
                        {
                            PostId = existingPost.PostID,
                            Link = result.Item1,  // Lưu file và lấy đường dẫn
                            Caption = "", // Chú thích cho ảnh
                            Index = index++, // Chỉ số của ảnh
                            Post = existingPost // Liên kết PostPhoto với Post
                        };

                        // Thêm ảnh vào danh sách ảnh của bài post
                        existingPost.PostPhotos.Add(photo);
                        postPhotosToSave.Add(photo); } 
                        
                        // Lưu danh sách ảnh sau khi cập nhật
                
                        if (postPhotosToSave.Any())
                            {
                            foreach (var postPhoto in postPhotosToSave)
                                {
                                    await _unitOfWork.PostPhotoDAO.AddNewPostPhoto(postPhoto);  // Lưu từng PostPhoto
                                }
                        }
                        
                    }
                // Cập nhật bài viết trong cơ sở dữ liệu
                await _unitOfWork.PostCategoryDA0.AddPostCategory(postCategory);
                await _unitOfWork.JobTitleDao.AddJobTitle(jobTitle);
                existingPost.JobId = jobTitle.JobId;
                //existingPost.PostCategoryId = postCategory.PostCategoryId;
                await _unitOfWork.PostDAO.UpdatePost(existingPost);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = existingPost;
                commonResponse.Status = 200;
                commonResponse.Message = UpdatePostSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 405;
            }
            return commonResponse;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _unitOfWork.PostDAO.GetPostById(postId);
            // Check if the post exists before updating
            if (post == null)
            {
                return false; // Return false if the post doesn't exist
            }
            post.PrivateLevel = 0;
            try
            {
                await _unitOfWork.PostDAO.UpdatePost(post);
                await _unitOfWork.CommitAsync();
                return true; // Return true on success

            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine("Error deleting post: " + ex.Message);
                return false; // Return false in case of an unexpected error
            }
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetPostResponse>> GetAllPostsAsync(HttpContext httpContext)
        {
            try
            {
                // Retrieve all posts
                List<Post> allPosts = await _unitOfWork.PostDAO.GetPosts();
                // Initialize a list for GetPostResponse objects
                List<GetPostResponse> postResponses = new List<GetPostResponse>();

                foreach (var post in allPosts)
                {
                    // Get post photos
                    var postPhotos = await _unitOfWork.PostPhotoDAO.GetAllById(post.PostID);
                    List<string> photoLinks = postPhotos.Select(photo => photo?.Link).Where(link => !string.IsNullOrEmpty(link)).ToList();

                    // Get post skills
                    var postSkills = await _unitOfWork.SkillCategoryDAO.GetAllById(post.PostID);
                    List<int> skillCategoryIds = postSkills.Select(skill => skill.SkillCategoryId ?? 0).ToList();

                    // Fetch skill categories by ID
                    List<SkillCategory> skillCategories = new List<SkillCategory>();
                    foreach (var skillCategoryId in skillCategoryIds)
                    {
                        var skillCategory = await _unitOfWork.SkillCategoryDAO.GetById(skillCategoryId);
                        if (skillCategory != null)
                        {
                            skillCategories.Add(skillCategory);
                        }
                    }

                    // Extract skill names
                    List<string> skillNames = skillCategories.Select(skill => skill.Name).ToList();

                    List<CommentDto> comments = new List<CommentDto>();
                    foreach (var comment in post.Comments)
                    {
                        if(comment.Status == 1)
                        {
                            CommentDto commentDto = new CommentDto();
                            commentDto.CommentId = comment.CommentId;
                            commentDto.PostId = comment.PostId;
                            commentDto.Content = comment.Content;
                            commentDto.CreatedOn = comment.CreatedOn;
                            commentDto.Account = _unitOfWork.AccountDao.GetAll().Where(x => x.AccountId == comment.AccountId).FirstOrDefault();

                            comments.Add(commentDto);
                        }
                    }

                    Account account = _unitOfWork.AccountDao.GetAll().Where(x => x.AccountId == post.CreatedBy).FirstOrDefault();

                    // Map post data to GetPostResponse object
                    GetPostResponse getPostResponse = new GetPostResponse
                    {
                        PostID = post.PostID,
                        Category = post.PostCategory?.Name,
                        jobTitle = post.Job?.Name,
                        Comments = comments,
                        Likes = post.Likes,
                        Content = post.Content,
                        Photo = photoLinks,
                        Skill = skillNames,
                        Account = account,
                        CreatedOn = post.CreatedOn,
                    };

                    // Add response to the list
                    postResponses.Add(getPostResponse);
                }

                return postResponses;
            }
            catch (Exception ex)
            {
                // Log and rethrow the exception with a detailed error message
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
            }
        }
        public async Task<List<Post>> GetPostByUserName(string username)
        {
            try
            {   
                var User = await _unitOfWork.AccountDAO.GetAccountByName(username);
                var postByUsername = await _unitOfWork.PostDAO.GetPostsByUserid(User.AccountId);
                //List<Post> allPosts = await _unitOfWork.PostDAO.GetPosts();
                return postByUsername;


            }
            catch (Exception ex)
            {
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
            }
        }

        public async Task<List<GetPostResponse>> GetAllPostsByUser(HttpContext httpContext)
        {
            JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
            string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
            var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);
            
            try 
            { 
            var allPosts = await _unitOfWork.PostDAO.GetPostsByUserid(accountStaff.AccountId);
            List<GetPostResponse> postResponses = new List<GetPostResponse>();

            foreach (var post in allPosts)
            {
                // Get post photos
                var postPhotos = await _unitOfWork.PostPhotoDAO.GetAllById(post.PostID);
                List<string> photoLinks = postPhotos.Select(photo => photo?.Link).Where(link => !string.IsNullOrEmpty(link)).ToList();

                // Get post skills
                var postSkills = await _unitOfWork.SkillCategoryDAO.GetAllById(post.PostID);
                List<int> skillCategoryIds = postSkills.Select(skill => skill.SkillCategoryId ?? 0).ToList();

                // Fetch skill categories by ID
                List<SkillCategory> skillCategories = new List<SkillCategory>();
                foreach (var skillCategoryId in skillCategoryIds)
                {
                    var skillCategory = await _unitOfWork.SkillCategoryDAO.GetById(skillCategoryId);
                    if (skillCategory != null)
                    {
                        skillCategories.Add(skillCategory);
                    }
                }

                // Extract skill names
                List<string> skillNames = skillCategories.Select(skill => skill.Name).ToList();

                List<CommentDto> comments = new List<CommentDto>();
                foreach (var comment in post.Comments)
                {
                    if (comment.Status == 1)
                    {
                        CommentDto commentDto = new CommentDto();
                        commentDto.CommentId = comment.CommentId;
                        commentDto.PostId = comment.PostId;
                        commentDto.Content = comment.Content;
                        commentDto.CreatedOn = comment.CreatedOn;
                        commentDto.Account = _unitOfWork.AccountDao.GetAll().Where(x => x.AccountId == comment.AccountId).FirstOrDefault();

                        comments.Add(commentDto);
                    }
                }

                Account account = _unitOfWork.AccountDao.GetAll().Where(x => x.AccountId == post.CreatedBy).FirstOrDefault();

                // Map post data to GetPostResponse object
                GetPostResponse getPostResponse = new GetPostResponse
                {
                    PostID = post.PostID,
                    Category = post.PostCategory?.Name,
                    jobTitle = post.Job?.Name,
                    Comments = comments,
                    Likes = post.Likes,
                    Content = post.Content,
                    Photo = photoLinks,
                    Skill = skillNames,
                    Account = account,
                    CreatedOn = post.CreatedOn,
                };

                // Add response to the list
                postResponses.Add(getPostResponse);
            }

            return postResponses;
            }
            catch (Exception ex)
            {
                // Log and rethrow the exception with a detailed error message
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
    }
}
    }
}
