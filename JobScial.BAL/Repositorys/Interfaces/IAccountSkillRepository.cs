using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IAccountSkillRepository
    {
        Task<bool> Delete(int AccountId, int Id);
        Task<bool> Save(AccountSkillDto ent);
        Task<List<AccountSkillDto>> GetAll(int AccountId);
        Task<AccountSkillDto> GetByID(int AccountId, int Id);
    }
}
