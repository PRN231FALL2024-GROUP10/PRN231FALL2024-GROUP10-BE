using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Posts;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace JobScial.WebAPI.Controllers
{
    public class PostsController : ODataController
    {
        private IPostRepository _postRepository;
        private readonly ILogger<PostsController> _logger;


        public PostsController(IPostRepository postRepository, ILogger<PostsController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
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

                commonResponse = await this._postRepository.AddPostAsync(createPostRequest, HttpContext);
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
            List<Post> products = await this._postRepository.GetAllPostsAsync(HttpContext);
            if (products == null || !products.Any())
            {
                return NotFound(new { Message = "No active posts found." });
            }
            return Ok(products);
        }
        [HttpPut("Post/{key}/UpdatePost")]
        //[PermissionAuthorize("Customer", "Store Owner")]
        public async Task<IActionResult> Put([FromRoute] int key, [FromForm] UpdatePostRequest updatePostRequest)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {
            
                commonResponse = await this._postRepository.UpdatePostAsync(updatePostRequest, HttpContext, key);
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
    }
}

