using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Posts;
using JobScial.BAL.DTOs.FireBase;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JobScial.BAL.Authorization;

namespace JobScial.WebAPI.Controllers
{
    public class AccountsController : ODataController
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private IValidator<RegisterRequest> _registerValidator;
        private IAccountRepository _accountRepository;
        private IEmailRepository _emailRepository;
        private IOptions<FireBaseImage> _firebaseImageOptions;
        public AccountsController(UserManager<IdentityUser> userManager, IAccountRepository accountRepository, IUnitOfWork unitOfWork, IEmailRepository emailRepository,
           IOptions<FireBaseImage> firebaseImageOptions)
        {
            _accountRepository = accountRepository;
            _emailRepository = emailRepository;
            _userManager = userManager;
            _firebaseImageOptions = firebaseImageOptions;
        }

        [HttpGet("Accounts")]
        public async Task<IActionResult> GetAccount()
        {
            try
            {
                return Ok(await _accountRepository.Get(HttpContext));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }

        [HttpGet("Admin/Accounts")]
        [PermissionAuthorize("2", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAccountAdmin()
        {
            try
            {
                return Ok(_accountRepository.AdminGet());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }

        [HttpPost("Accounts/{accId}/follow")]
        public async Task<IActionResult> FollowAccount([FromRoute] int accId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await this._accountRepository.ConnectAccount(accId, HttpContext);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Update Follow Successfully");
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

        [HttpPost("Accounts/image")]
        public async Task<IActionResult> SaveAvatar([FromForm] CreatePostRequest request)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await this._accountRepository.ChangeImageAsync(request, HttpContext, _firebaseImageOptions.Value);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "OK");

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

        }

        [HttpGet("accounts/{Id}")]
        public async Task<IActionResult> GetDetailsAccountById([FromRoute] int Id)
        {
            try
            {
                var account = await _accountRepository.GetProfileById(Id, HttpContext);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }

        #region Delete Account

        [HttpDelete("{accountId}")]
        [PermissionAuthorize("2", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAccount([FromRoute] int accountId)
        {
            try
            {
                await _accountRepository.DeleteAccount(accountId);

                return Ok(new { Message = "Account deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }
        #endregion
        #region Register
        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                IdentityUser user = new()
                {
                    Email = registerRequest.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerRequest.FullName,
                    TwoFactorEnabled = true
                };
                //Add Token to Verify the email....
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Sử dụng giao thức HTTPS với domain trên Azure
                //var scheme = "https";
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Accounts", new { token, email = user.Email }, Request.Scheme);
                var message = new JobScial.BAL.Models.Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
                _emailRepository.SendEmail(message);
                GetAccountResponse customer = await this._accountRepository
                .Register(registerRequest);

                return Ok(new
                {
                    Status = $"User created & Email sent to {customer.Email} Successfully",
                    Data = customer
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }

        #endregion
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _accountRepository.GetProfileByEmail(email);
            if (user != null)
            {
                await _accountRepository.ConfirmAccount(user.Account.AccountId);
                var viewResult = new ViewResult
                {
                    ViewName = "EmailConfirmationSuccess" // Tên view bạn muốn trả về
                };
                return viewResult;
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new JobScial.WebAPI.Models.Response { Status = "Error", Message = "This User Doesnot exist!" });
        }

        [HttpPost("Account/BanAccount/{Id}")]
        [PermissionAuthorize("2", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> BanAccount(int Id)
        {
            // Validate the input
            if (Id <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid account ID." });
            }

            try
            {
                // Attempt to ban the account
                bool isBanned = await _accountRepository.BanAccount(Id);

                if (isBanned)
                {
                    return Ok(new { Success = true, Message = "Account banned successfully." });
                }
                else
                {
                    return NotFound(new { Success = false, Message = $"Account with ID {Id} not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional: add logging logic here)
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }
        //        [HttpPost("Account/CreateNewAccount")]
        //        [EnableQuery]
        //        //[PermissionAuthorize("Staff")]
        //        public async Task<IActionResult> AddNewAccount([FromBody] AddNewAccount addNewAccount)
        //        {
        //            CommonResponse commonResponse = new CommonResponse();
        //            try
        //            {

        //                commonResponse = await this._accountRepository.AddNewAccount(addNewAccount);
        //                switch (commonResponse.Status)
        //                {
        //                    case 200:
        //                        return StatusCode(200, "Add Account Success");
        //                    //return Ok(commonResponse);
        //                    default:
        //                        return StatusCode(500, commonResponse);
        //                }
        //            }
        //            catch (BadRequestException ex)
        //            {
        //                return BadRequest(ex.Message);
        //            }
        ///*            catch (Exception ex)
        //            {
        //                // Xử lý các ngoại lệ khác
        //                return StatusCode(500, "Internal Server Error");
        //            }*/

        //        }
        [HttpPut("Account/{key}/UpdateAccount")]
        [PermissionAuthorize("2", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromRoute] int key, [FromBody] UpdateAccountRequest updateAccountRequest)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {

                commonResponse = await this._accountRepository.UpdateAccount(key, updateAccountRequest);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Update Account Success");
                    //return Ok(commonResponse);

                    default:
                        return StatusCode(500, commonResponse);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Account/UnlockAccount/{Id}")]
        [PermissionAuthorize("2", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UnlockAccount(int Id)
        {
            // Validate the input
            if (Id <= 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid account ID." });
            }

            try
            {

                // Attempt to ban the account
                bool isBanned = await _accountRepository.UnlockAccount(Id);

                if (isBanned)
                {
                    return Ok(new { Success = true, Message = "Account unlocked successfully." });
                }
                else
                {
                    return NotFound(new { Success = false, Message = $"Account with ID {Id} not found." });
                }


            }
            catch (Exception ex)
            {
                // Handle the error and return a 500 status code
                return StatusCode(500, new { Success = false, Message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
