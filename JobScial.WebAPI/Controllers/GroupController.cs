using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Group;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobScial.WebAPI.Controllers
{
    public class GroupController : Controller
    {
        public readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository  groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost("Group/AddNewGroup")]
        //[PermissionAuthorize("Staff")]
        public async Task<IActionResult> Post([FromBody] AddGroupRequest groupRequest)
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

                commonResponse = await this._groupRepository.AddGroupAsync(groupRequest);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add Group Success");
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
            return Ok(commonResponse); // Trả về phản hồi

        }

    }
}
