using BMOS.BAL.Exceptions;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Group;
using JobScial.BAL.DTOs.Posts;
using JobScial.BAL.Repositorys.Implementations;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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
        [HttpPost("Group/JoinNewGroup")]
        //[PermissionAuthorize("Staff")]
        public async Task<IActionResult> JoinNewGroup([FromBody] JoinGroupRequest joinGroupRequest)
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

                commonResponse = await this._groupRepository.JoinGroupAsync(joinGroupRequest);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add GroupMember Success");
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
        [EnableQuery]
        [HttpDelete("Group/DeleteGroup/{key}")]

        public async Task<IActionResult> DeleteComment([FromRoute] int key)
        {

            await this._groupRepository.DeleteGroupAsync(key);
            return Ok(new
            {
                Status = "Delete Comment Success"
            });
        }
        [HttpPut("Group/{key}/BanMemberGroup")]
        //[PermissionAuthorize("Customer", "Store Owner")]
        public async Task<IActionResult> BanMemberGroup([FromRoute] int key, [FromRoute] int id)
        {

            // Validate the input
            if (key <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid account ID." });
            }

            try
            {
                // Attempt to ban the account
                bool isBanned = await _groupRepository.BanMemberGroup(key,id);

                if (isBanned)
                {
                    return Ok(new { Success = true, Message = "Member banned successfully." });
                }
                else
                {
                    return NotFound(new { Success = false, Message = $"Member with ID {key} not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional: add logging logic here)
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }
        [HttpPut("Group/{key}/approveMember")]
        //[PermissionAuthorize("Customer", "Store Owner")]
        public async Task<IActionResult> Put([FromRoute] int key, [FromRoute] int Id)
        {

            // Validate the input
            if (key <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid account ID." });
            }

            try
            {
                // Attempt to ban the account
                bool isBanned = await _groupRepository.BanMemberGroup(key,Id);

                if (isBanned)
                {
                    return Ok(new { Success = true, Message = "Member approve successfully." });
                }
                else
                {
                    return NotFound(new { Success = false, Message = $"Member with ID {key} not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional: add logging logic here)
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
