using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.ComponentModel.DataAnnotations;

namespace JobScial.WebAPI.Controllers
{
    public class AccountsController : ODataController 
    {
        //private IValidator<RegisterRequest> _registerValidator;
        private IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        #region Register
        [HttpPost("odata/Accounts/Register")]
        [EnableQuery]
        public async Task<IActionResult> Post([FromForm] RegisterRequest registerRequest)
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
    }
}
