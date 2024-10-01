using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Authentications;
using JobScial.BAL.DTOs.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<PostLoginResponse> LoginAsync(PostAccountRequest account, JwtAuth jwtAuth);
        //Task<PostRecreateTokenResponse> ReCreateTokenAsync(PostRecreateTokenRequest token, JwtAuth jwtAuth);
    }
}
