using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class AccountExpRepository : IAccountExpRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AccountExpRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;

        }
        public async Task<bool> Delete(int AccountId, int Id)
        {
            try
            {
                var ent = _unitOfWork.AccountExperienceDao.GetAll().Where(x => x.AccountId == AccountId && x.CompanyId == Id).FirstOrDefault();

                if (ent == null)
                {
                    return false;
                }

                _unitOfWork.AccountExperienceDao.Delete(ent);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<List<AccountExperienceDto>> GetAll(int AccountId)
        {
            var lstAll = _unitOfWork.AccountExperienceDao.GetAll().Where(x => x.AccountId == AccountId).ToList();
            List<AccountExperienceDto> result = new List<AccountExperienceDto>();
            var lstCompany = _unitOfWork.CompanyDAO.GetAll();
            var lstTime = _unitOfWork.TimeSpanUnitDAO.GetAll();
            
            foreach (var ent in lstAll)
            {
                AccountExperienceDto cert = new AccountExperienceDto();
                var company = lstCompany.Where(x => x.CompanyId == ent.CompanyId).FirstOrDefault();
                var timespanunit = lstTime.Where(x => x.TimespanUnitId == ent.TimespanUnit).FirstOrDefault();
                cert.AccountId = ent.AccountId;
                cert.CompanyId = ent.CompanyId;
                cert.CompanyName = company == null ? "FPT" : company.Name + "";
                cert.Description = ent.Description;
                cert.JobTitle = ent.JobTitle;
                cert.TimespanUnit = ent.TimespanUnit;
                cert.TimespanUnitName = timespanunit == null ? "Year" : timespanunit.Name;
                cert.Timespan = ent.Timespan;
                cert.YearStart = ent.YearStart;

                result.Add(cert);
            }

            return result;
        }

        public async Task<AccountExperienceDto> GetByID(int AccountId, int Id)
        {
            var ent = _unitOfWork.AccountExperienceDao.GetAll().Where(x => x.AccountId == AccountId && x.CompanyId == Id).FirstOrDefault();
            AccountExperienceDto cert = new AccountExperienceDto();
            if (ent == null)
            {
                return cert;
            }

            var company = _unitOfWork.CompanyDAO.GetAll().Where(x => x.CompanyId == Id).FirstOrDefault();
            var timespanunit = _unitOfWork.TimeSpanUnitDAO.GetAll().Where(x => x.TimespanUnitId == ent.TimespanUnit).FirstOrDefault();
            

            cert.AccountId = ent.AccountId;
            cert.CompanyId = ent.CompanyId;
            cert.CompanyName = company == null? "FPT" : company.Name + "";
            cert.Description = ent.Description;
            cert.JobTitle = ent.JobTitle;
            cert.TimespanUnit = ent.TimespanUnit;
            cert.TimespanUnitName = timespanunit == null? "Year" : timespanunit.Name;
            cert.Timespan = ent.Timespan;
            cert.YearStart = ent.YearStart;

            return cert;
        }

        public async Task<bool> Save(AccountExperienceDto ent)
        {
            try
            {
                var obj = _unitOfWork.AccountExperienceDao
                    .GetAll()
                    .Where(x => x.AccountId == ent.AccountId && x.CompanyId == ent.CompanyId).FirstOrDefault();

                if (obj == null)
                {
                    AccountExperience a = new AccountExperience();
                    a.AccountId = ent.AccountId;
                    a.CompanyId = ent.CompanyId;
                    a.Description = ent.Description;
                    a.JobTitle = ent.JobTitle;
                    a.TimespanUnit = ent.TimespanUnit;
                    a.Timespan = ent.Timespan;
                    a.YearStart = ent.YearStart;

                    _unitOfWork.AccountExperienceDao.Add(a);
                }
                else
                {
                    obj.Description = ent.Description;
                    obj.JobTitle = ent.JobTitle;
                    obj.TimespanUnit = ent.TimespanUnit;
                    obj.Timespan = ent.Timespan;
                    obj.YearStart = ent.YearStart;
                    _unitOfWork.AccountExperienceDao.Update(obj);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
