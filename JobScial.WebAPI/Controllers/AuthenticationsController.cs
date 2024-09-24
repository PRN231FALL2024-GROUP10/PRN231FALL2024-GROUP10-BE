using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.JWT;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Options;


namespace JobScial.WebAPI.Controllers
{
    public class AuthenticationsController : ODataController
    {
        private IOptions<JwtAuth> _jwtAuthOptions;
        private IAuthenticationRepository _authenticationRepository;


        #region Login
        [EnableQuery]
        [HttpPost("odata/authentications/login")]
        public async Task<IActionResult> Login([FromBody] PostAccountRequest request)
        {
            /*var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                string error = ErrorHelper.GetErrorsString(validationResult);
                throw new BadRequestException(error);
            }*/

            var result = await _authenticationRepository.LoginAsync(request, _jwtAuthOptions.Value);
            return Ok(result);
        }
        #endregion
    }
}
