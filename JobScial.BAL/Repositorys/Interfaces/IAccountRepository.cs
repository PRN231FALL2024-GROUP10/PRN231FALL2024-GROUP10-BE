using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.FireBase;
using JobScial.BAL.DTOs.Profile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IAccountRepository
    {
        public Task<AccountProfileDto> GetProfileById(int accountId, HttpContext httpContext);
        public Task<AccountProfileDto> GetProfileByEmail(string email);
        public Task<AccountProfileDto> UpdateProfile(string email, UpdateProfileDto profile);
        public Task<GetAccountResponse> Register(RegisterRequest registerRequest);
        public Task DeleteAccount(int accountId);
        public Task<List<AccountDto>> Get(HttpContext httpContext);
        public Task<List<AccountDto>> GetByProfileFollow(int accountId, HttpContext httpContext);
        public Task<List<AccountDto>> AdminGet();
        Task<CommonResponse> ConnectAccount(int accountId, HttpContext httpContext);
        Task<CommonResponse> ChangeImageAsync(CreatePostRequest post, HttpContext httpContext, FireBaseImage fireBaseImage);
        public Task<bool> ConfirmAccount(int accountId);
        public Task<bool> BanAccount(int accountId);
        public Task<bool> UnlockAccount(int accountId);
        public Task<CommonResponse> UpdateAccount(int AccountId, UpdateAccountRequest updateAccountRequest);
    }
}
