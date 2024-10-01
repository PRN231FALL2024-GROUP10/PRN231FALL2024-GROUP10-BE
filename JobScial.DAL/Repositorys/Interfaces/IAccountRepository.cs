using JobScial.BAL.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Interfaces
{
    public interface IAccountRepository
    {
        public Task<GetAccountResponse> Register(RegisterRequest registerRequest);

    }
}
