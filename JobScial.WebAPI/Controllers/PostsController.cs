using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
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


        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        [HttpPost("odata/Post/AddNewPost")]
        [EnableQuery]
        //[PermissionAuthorize("Staff")]
        public async Task<IActionResult> Post([FromForm] CreatePostRequest createPostRequest)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                /*var resultValid = await _postValidator.ValidateAsync(addPostRequest);
                if (!resultValid.IsValid)
                {
                    string error = ErrorHelper.GetErrorsString(resultValid);
                    throw new BadRequestException(error);
                }*/
                commonResponse = await this._postRepository.AddPostAsync(createPostRequest, HttpContext);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add Post Success");
                    //return Ok(commonResponse);
                    case 405:
                        return StatusCode(405, "Method Not Allowed: This URL picture not safe to post .");

                    default:
                        return StatusCode(500, commonResponse);
                }
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("odata/Posts/Active/Post")]
        [EnableQuery]
        public async Task<IActionResult> ActivePosts()
        {
            List<Post> products = await this._postRepository.GetAllPostsAsync(HttpContext);
            if (products == null || !products.Any())
            {
                return NotFound(new { Message = "No active posts found." });
            }
            return Ok(products);
        }
        [HttpPut("Post/{key}/UpdatePost")]
        [EnableQuery]
        //[PermissionAuthorize("Customer", "Store Owner")]
        public async Task<IActionResult> Put([FromRoute] int key, [FromForm] CreatePostRequest updatePostRequest)
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

