using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using BMOS.DAL.Enums;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Authentications;
using JobScial.BAL.DTOs.JWT;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AuthenticationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            
        }
        #region Login
        public async Task<PostLoginResponse> LoginAsync(PostAccountRequest request, JwtAuth jwtAuth)
        {
            try
            {
                //var account = await _unitOfWork.AccountDAO.GetAccountByEmailAndPasswordAsync(request.Email, StringHelper.EncryptData(request.PasswordHash.Trim()));
                var account = await _unitOfWork.AccountDAO.GetAccountByEmail(request.Email);
                if (account == null)
                {
                    throw new BadRequestException("Email or password is invalid.");
                }

                var loginResponse = new PostLoginResponse();
                loginResponse.AccountId = account.AccountId;
                loginResponse.Email = account.Email;
                loginResponse.Role = account.Role;
                loginResponse.FullName = account.FullName;


                var resultLogin = await GenerateToken(loginResponse, jwtAuth, account);
                return resultLogin;
            }
            catch (BadRequestException ex)
            {
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new BadRequestException(error);
            }
            catch (Exception ex)
            {
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
            }
        }
        #endregion
        #region Generate token
        private async Task<PostLoginResponse> GenerateToken(PostLoginResponse response, JwtAuth jwtAuth, Account account)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuth.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new ClaimsIdentity(new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, response.Email),
                 new Claim(JwtRegisteredClaimNames.Email, response.Email),
                 new Claim(JwtRegisteredClaimNames.Name, response.FullName),
                 //new Claim("Role", response.Role),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             });

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Issuer = jwtAuth.Issuer,
                    Audience = jwtAuth.Audience,
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = credentials,
                };

                var token = jwtTokenHandler.CreateToken(tokenDescription);
                string accessToken = jwtTokenHandler.WriteToken(token);

                string refreshToken = GenerateRefreshToken();
                

                await _unitOfWork.CommitAsync();

                response.AccessToken = accessToken;

                return response;
            }
            catch (Exception ex)
            {
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
            }
        }
        #endregion
        #region Generate refresh token
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        #endregion
    }
}
