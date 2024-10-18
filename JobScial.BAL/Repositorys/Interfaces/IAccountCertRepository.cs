using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IAccountCertRepository
    {
        Task<bool> Delete(int AccountId, int Id);
        Task<bool> Save(AccountCertificateDto ent);
        Task<List<AccountCertificateDto>> GetAll(int AccountId);
        Task<AccountCertificateDto> GetByID(int AccountId, int Id);
    }
}
