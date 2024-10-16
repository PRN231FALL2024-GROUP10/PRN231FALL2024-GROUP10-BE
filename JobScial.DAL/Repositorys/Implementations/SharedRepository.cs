using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Shared;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class SharedRepository : ISharedRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public SharedRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;

        }

        public async Task<List<CompanyDto>> GetAllCompany()
        {
            var lstAll = _unitOfWork.CompanyDAO.GetAll().ToList();
            List<CompanyDto> result = new List<CompanyDto>();

            foreach (var ent in lstAll)
            {
                CompanyDto o = new CompanyDto();
                o.ID= ent.CompanyId;
                o.Name= ent.Name;
                result.Add(o);
            }

            return result;
        }

        public async Task<List<JobTitleDto>> GetAllJobTitle()
        {
            var lstAll = _unitOfWork.JobTitleDAO.GetAll().ToList();
            List<JobTitleDto> result = new List<JobTitleDto>();

            foreach (var ent in lstAll)
            {
                JobTitleDto o = new JobTitleDto();
                o.ID = ent.JobId;
                o.Name = ent.Name;
                result.Add(o);
            }

            return result;
        }

        public async Task<List<PostCategoryDto>> GetAllPostCategory()
        {
            var lstAll = _unitOfWork.PostCategoryDao.GetAll().ToList();
            List<PostCategoryDto> result = new List<PostCategoryDto>();

            foreach (var ent in lstAll)
            {
                PostCategoryDto o = new PostCategoryDto();
                o.ID = ent.PostCategoryId;
                o.Name = ent.Name;
                result.Add(o);
            }

            return result;
        }

        public async Task<List<SchoolDto>> GetAllSchool()
        {
            var lstAll = _unitOfWork.SchoolDAO.GetAll().ToList();
            List<SchoolDto> result = new List<SchoolDto>();

            foreach (var ent in lstAll)
            {
                SchoolDto o = new SchoolDto();
                o.ID = ent.SchoolId;
                o.Name = ent.Name;
                o.Description = ent.Description;
                result.Add(o);
            }

            return result;
        }

        public async Task<List<SkillDto>> GetAllSkillCategory()
        {
            var lstAll = _unitOfWork.SkillDAO.GetAll().ToList();
            List<SkillDto> result = new List<SkillDto>();

            foreach (var ent in lstAll)
            {
                SkillDto o = new SkillDto();
                o.ID = ent.SkillCategoryId;
                o.Name = ent.Name;
                result.Add(o);
            }

            return result;
        }

        public async Task<List<TimeSpanDto>> GetAllTimespanUnit()
        {
            var lstAll = _unitOfWork.TimeSpanUnitDAO.GetAll().ToList();
            List<TimeSpanDto> result = new List<TimeSpanDto>();

            foreach (var ent in lstAll)
            {
                TimeSpanDto o = new TimeSpanDto();
                o.ID = ent.TimespanUnitId;
                o.Name = ent.Name;
                result.Add(o);
            }

            return result;
        }

        public async Task<CompanyDto> GetByCompanyID(int Id)
        {
            var ent = _unitOfWork.CompanyDAO.GetAll().Where(x => x.CompanyId == Id).FirstOrDefault();
            CompanyDto o = new CompanyDto();
            o.ID = ent.CompanyId;
            o.Name = ent.Name;

            return o;
        }

        public async Task<JobTitleDto> GetByJobTitleID(int Id)
        {
            var ent = _unitOfWork.JobTitleDAO.GetAll().Where(x => x.JobId == Id).FirstOrDefault();
            JobTitleDto o = new JobTitleDto();
            o.ID = ent.JobId;
            o.Name = ent.Name;

            return o;
        }

        public async Task<PostCategoryDto> GetByPostCategoryID(int Id)
        {
            var ent = _unitOfWork.PostCategoryDao.GetAll().Where(x => x.PostCategoryId == Id).FirstOrDefault();
            PostCategoryDto o = new PostCategoryDto();
            o.ID = ent.PostCategoryId;
            o.Name = ent.Name;

            return o;
        }

        public async Task<SchoolDto> GetBySchoolID(int Id)
        {
            var ent = _unitOfWork.SchoolDAO.GetAll().Where(x => x.SchoolId == Id).FirstOrDefault();
            SchoolDto o = new SchoolDto();
            o.ID = ent.SchoolId;
            o.Name = ent.Name;
            o.Description = ent.Description;
            return o;
        }

        public async Task<SkillDto> GetBySkillCategoryID(int Id)
        {
            var ent = _unitOfWork.SkillDAO.GetAll().Where(x => x.SkillCategoryId == Id).FirstOrDefault();
            SkillDto o = new SkillDto();
            o.ID = ent.SkillCategoryId;
            o.Name = ent.Name;

            return o;
        }

        public async Task<TimeSpanDto> GetByTimespanUnitID(int Id)
        {
            var ent = _unitOfWork.TimeSpanUnitDAO.GetAll().Where(x => x.TimespanUnitId == Id).FirstOrDefault();
            TimeSpanDto o = new TimeSpanDto();
            o.ID = ent.TimespanUnitId;
            o.Name = ent.Name;

            return o;
        }
    }
}
