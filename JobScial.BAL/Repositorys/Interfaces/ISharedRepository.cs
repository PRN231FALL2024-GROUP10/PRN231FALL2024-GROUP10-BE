using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Shared;
using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface ISharedRepository
    {
        Task<List<SchoolDto>> GetAllSchool();
        Task<SchoolDto> GetBySchoolID(int Id);

        Task<List<CompanyDto>> GetAllCompany();
        Task<CompanyDto> GetByCompanyID(int Id);

        Task<List<SkillDto>> GetAllSkillCategory();
        Task<SkillDto> GetBySkillCategoryID(int Id);

        Task<List<TimeSpanDto>> GetAllTimespanUnit();
        Task<TimeSpanDto> GetByTimespanUnitID(int Id);

        Task<List<PostCategoryDto>> GetAllPostCategory();
        Task<PostCategoryDto> GetByPostCategoryID(int Id);

        Task<List<JobTitleDto>> GetAllJobTitle();
        Task<JobTitleDto> GetByJobTitleID(int Id);
    }
}
