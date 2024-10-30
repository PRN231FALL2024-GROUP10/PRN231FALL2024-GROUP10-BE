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

namespace JobScial.WebAPI.Controllers
{
    public class AccountsController : ODataController 
    {
        //private IValidator<RegisterRequest> _registerValidator;
        private IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("Accounts")]
        public async Task<IActionResult> GetAccount()
        {
            try
            {
                return Ok(_accountRepository.Get());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }

        #region Delete Account

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int accountId)
        {
            try
            {
                _accountRepository.DeleteAccount(accountId);

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
                /*ValidationResult validationResult = await _registerValidator.ValidateAsync(registerRequest);
                if (!validationResult.IsValid)
                {
                    string error = ErrorHelper.GetErrorsString(validationResult);
                    throw new BadRequestException(error);
                }*/
                GetAccountResponse customer = await this._accountRepository
                .Register(registerRequest);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }
        #endregion
        [HttpPost("Account/BanAccount/{Id}")]
        [EnableQuery]
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
        [HttpPost("Account/CreateNewAccount")]
        [EnableQuery]
        //[PermissionAuthorize("Staff")]
        public async Task<IActionResult> AddNewAccount([FromBody] AddNewAccount addNewAccount)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                commonResponse = await this._accountRepository.AddNewAccount(addNewAccount);
                switch (commonResponse.Status)
                {
                    case 200:
                        return StatusCode(200, "Add Account Success");
                    //return Ok(commonResponse);
                    default:
                        return StatusCode(500, commonResponse);
                }
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
/*            catch (Exception ex)
            {
                // Xử lý các ngoại lệ khác
                return StatusCode(500, "Internal Server Error");
            }*/

        }
        [HttpPut("Account/{key}/UpdateAccount")]
        [EnableQuery]
        //[PermissionAuthorize("Customer", "Store Owner")]
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
