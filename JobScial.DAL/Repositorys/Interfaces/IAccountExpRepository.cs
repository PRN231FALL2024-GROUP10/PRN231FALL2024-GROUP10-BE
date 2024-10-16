using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Interfaces
{
    public interface IAccountExpRepository
    {
        Task<bool> Delete(int AccountId, int Id);
        Task<bool> Save(AccountExperienceDto ent);
        Task<List<AccountExperienceDto>> GetAll(int AccountId);
        Task<AccountExperienceDto> GetByID(int AccountId, int Id);
    }
}
