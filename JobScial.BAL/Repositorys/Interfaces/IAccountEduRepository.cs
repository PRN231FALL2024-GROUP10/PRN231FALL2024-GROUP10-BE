using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IAccountEduRepository
    {
        Task<bool> Delete(int AccountId, int Id);
        Task<bool> Save(AccountEducationDto ent);
        Task<List<AccountEducationDto>> GetAll(int AccountId);
        Task<AccountEducationDto> GetByID(int AccountId, int Id);
    }
}
