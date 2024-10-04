using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobScial.WebAPI.Controllers
{
    public class AccountsController : ODataController 
    {
        //private IValidator<RegisterRequest> _registerValidator;
        private IAccountRepository _accountRepository;
        private readonly IUnitOfWork unitOfWork;

        public AccountsController(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            this.unitOfWork = unitOfWork;
        }

        [EnableQuery(PageSize = 2)]
        public IActionResult Get()
        {
            return Ok(unitOfWork.AccountDao.GetAll());
        }

        public IActionResult Get([FromRoute] int key)
        {
            var e = unitOfWork.AccountDao.FindOne(p => p.AccountId == key);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }


        #region Register
        [HttpPost("Register")]
        [EnableQuery]
        public async Task<IActionResult> Post([FromForm] RegisterRequest registerRequest)
        {
            /*ValidationResult validationResult = await _registerValidator.ValidateAsync(registerRequest);
            if (!validationResult.IsValid)
            {
                string error = ErrorHelper.GetErrorsString(validationResult);
                throw new BadRequestException(error);
            }*/
            GetAccountResponse customer = await this._accountRepository
                .Register(registerRequest);

            
            return Ok();
        }
        #endregion
    }
}
