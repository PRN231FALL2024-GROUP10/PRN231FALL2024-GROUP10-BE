using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Posts;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using JobScial.BAL.DTOs.FireBase;

namespace JobScial.WebAPI.Controllers
{
    public class PostsController : ODataController
    {
        private IPostRepository _postRepository;
        private readonly ILogger<PostsController> _logger;
        private IOptions<FireBaseImage> _firebaseImageOptions;


        public PostsController(IPostRepository postRepository, 
            ILogger<PostsController> logger,
            IOptions<FireBaseImage> firebaseImageOptions)
        {
            _postRepository = postRepository;
            _logger = logger;
            _firebaseImageOptions = firebaseImageOptions;

        }


        [HttpPost("Post/AddNewPost")]
        //[PermissionAuthorize("Staff")]
        public async Task<IActionResult> Post([FromForm] CreatePostRequest createPostRequest)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                // Ghi nhận thông tin
                /*var resultValid = await _postValidator.ValidateAsync(addPostRequest);
                if (!resultValid.IsValid)
                {
                    string error = ErrorHelper.GetErrorsString(resultValid);
                    throw new BadRequestException(error);
                }*/
                // Kiểm tra xem có dữ liệu trong createPostRequest không

                commonResponse = await this._postRepository.AddPostAsync(createPostRequest,_firebaseImageOptions.Value , HttpContext);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add Post Success");
                    //return Ok(commonResponse);

                    default:
                        return StatusCode(500, commonResponse);
                }

            }

            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // Xử lý các exception không mong muốn khác
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
            return Ok(); // Trả về phản hồi

        }

        [HttpGet("Post/Active")]
        [EnableQuery]
        public async Task<IActionResult> GetActivePosts()
        {
            // Retrieve all posts, mapping to GetPostResponse
            List<GetPostResponse> activePosts = await _postRepository.GetAllPostsAsync(HttpContext);

            // Check if posts exist
            if (activePosts == null || !activePosts.Any())
            {
                return NotFound(new { Message = "No active posts found." });
            }

            // Return the list of active posts
            return Ok(activePosts);
        }
        [HttpGet("Post/PostByUser")]
        [EnableQuery]
        public async Task<IActionResult> GetActivePostsByUser()
        {
            // Retrieve all posts, mapping to GetPostResponse
            List<GetPostResponse> activePosts = await _postRepository.GetAllPostsByUser(HttpContext);

            // Check if posts exist
            if (activePosts == null || !activePosts.Any())
            {
                return NotFound(new { Message = "No active posts found." });
            }

            // Return the list of active posts
            return Ok(activePosts);
        }
        [HttpPut("Post/{key}/UpdatePost")]
        //[PermissionAuthorize("Customer", "Store Owner")]
        public async Task<IActionResult> Put([FromRoute] int key, [FromForm] UpdatePostRequest updatePostRequest)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {
            
                commonResponse = await this._postRepository.UpdatePostAsync(updatePostRequest, _firebaseImageOptions.Value, HttpContext, key);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Update Post Success");
                    case 405:
                        return StatusCode(405, "Method Not Allowed: This URL picture not safe to post .");
                    default:
                        return StatusCode(500, commonResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Post/GetPostByUserName")]
        //[PermissionAuthorize("Customer", "Store Owner")]
        public async Task<IActionResult> GetPostUser(string name)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {

                var listPost = await this._postRepository.GetPostByUserName(name);
                if (listPost == null || !listPost.Any())
                {
                    commonResponse.Status = 404;
                    commonResponse.Message = "No posts found for this user.";
                }
                else
                {
                    commonResponse.Status = 200;
                    commonResponse.Message = "Posts retrieved successfully.";
                    commonResponse.Data = listPost; // Trả về danh sách bài viết
                }

                switch (commonResponse.Status)
                {
                    case 200:
                        return Ok(commonResponse); // Trả về danh sách bài post
                    case 404:
                        return StatusCode(404, commonResponse.Message);
                    case 405:
                        return StatusCode(405, "Method Not Allowed: This URL picture not safe to post.");
                    default:
                        return StatusCode(500, commonResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

