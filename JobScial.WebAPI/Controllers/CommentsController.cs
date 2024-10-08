using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Comments;
using JobScial.DAL.Repositorys.Implementations;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace JobScial.WebAPI.Controllers
{
    public class CommentsController : ODataController
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost("odata/Comment/AddNewComment")]
        [EnableQuery]
        //[PermissionAuthorize("Staff")]
        public async Task<IActionResult> Post([FromForm] CreateCommentRequest createCommentRequest)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await this._commentRepository.AddCommentAsync(createCommentRequest, HttpContext);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add Comment Success");
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
    }
}
