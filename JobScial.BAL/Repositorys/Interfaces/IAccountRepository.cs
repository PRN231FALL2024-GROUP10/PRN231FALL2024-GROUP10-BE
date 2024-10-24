using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IAccountRepository
    {
        public Task<AccountProfileDto> GetProfileById(int accountId);
        public Task<AccountProfileDto> GetProfileByEmail(string email);
        public Task<AccountProfileDto> UpdateProfile(string email, UpdateProfileDto profile);
        public Task<GetAccountResponse> Register(RegisterRequest registerRequest);
        public void DeleteAccount(int accountId);
        public Task<List<AccountDto>> Get();

        public Task<bool> BanAccount(int accountId);

        public Task<bool> UnlockAccount(int accountId);

    }
}
