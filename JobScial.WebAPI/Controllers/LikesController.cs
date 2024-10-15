using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Comments;
using JobScial.BAL.DTOs.Posts;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace JobScial.WebAPI.Controllers
{
    public class LikesController : ODataController
    {
        private readonly IlikeRepository _likeRepository;

        public LikesController(IlikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        [HttpPost("Like/AddNewLike")]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] CreateLike createLike)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await this._likeRepository.AddLikeAsync(createLike, HttpContext);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add Like Success");
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
