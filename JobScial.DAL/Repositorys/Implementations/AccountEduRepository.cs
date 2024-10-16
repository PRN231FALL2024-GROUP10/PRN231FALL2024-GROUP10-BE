using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class AccountEduRepository : IAccountEduRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AccountEduRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;

        }
        public async Task<bool> Delete(int AccountId, int Id)
        {
            try
            {
                var ent = _unitOfWork.AccountEducationDao.GetAll().Where(x => x.AccountId == AccountId && x.SchoolId == Id).FirstOrDefault();

                if (ent == null)
                {
                    return false;
                }

                _unitOfWork.AccountEducationDao.Delete(ent);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<List<AccountEducationDto>>  GetAll(int AccountId)
        {
            var lstAll = _unitOfWork.AccountEducationDao.GetAll().Where(x => x.AccountId == AccountId).ToList();
            List<AccountEducationDto> result = new List<AccountEducationDto>();
            var lstSchool = _unitOfWork.SchoolDAO.GetAll();
            var lstTime = _unitOfWork.TimeSpanUnitDAO.GetAll();

            foreach (var ent in lstAll)
            {
                AccountEducationDto cert = new AccountEducationDto();
                var school = lstSchool.Where(x => x.SchoolId == ent.SchoolId).FirstOrDefault();
                var timespanunit = lstTime.Where(x => x.TimespanUnitId == ent.TimespanUnit).FirstOrDefault();
                cert.AccountId = ent.AccountId;
                cert.SchoolId = ent.SchoolId;
                cert.SchoolName = school == null ? "FPT" : school.Name + "";
                cert.Description = ent.Description;
                cert.TimespanUnit = ent.TimespanUnit;
                cert.TimespanUnitName = timespanunit == null ? "Year" : timespanunit.Name;
                cert.Timespan = ent.Timespan;
                cert.YearStart = ent.YearStart;

                result.Add(cert);
            }

            return result;
        }

        public async Task<AccountEducationDto>  GetByID(int AccountId, int Id)
        {
            var ent = _unitOfWork.AccountEducationDao.GetAll().Where(x => x.AccountId == AccountId && x.SchoolId == Id).FirstOrDefault();
            AccountEducationDto cert = new AccountEducationDto();
            if (ent == null)
            {
                return cert;
            }

            var school = _unitOfWork.SchoolDAO.GetAll().Where(x => x.SchoolId == ent.SchoolId).FirstOrDefault();
            var timespanunit = _unitOfWork.TimeSpanUnitDAO.GetAll().Where(x => x.TimespanUnitId == ent.TimespanUnit).FirstOrDefault();


            cert.AccountId = ent.AccountId;
            cert.SchoolId = ent.SchoolId;
            cert.SchoolName = school == null ? "FPT" : school.Name + "";
            cert.Description = ent.Description;
            cert.TimespanUnit = ent.TimespanUnit;
            cert.TimespanUnitName = timespanunit == null ? "Year" : timespanunit.Name;
            cert.Timespan = ent.Timespan;
            cert.YearStart = ent.YearStart;

            return cert;
        }

        public async Task<bool> Save(AccountEducationDto ent)
        {
            try
            {
                var obj = _unitOfWork.AccountEducationDao
                    .GetAll()
                    .Where(x => x.AccountId == ent.AccountId && x.SchoolId == ent.SchoolId).FirstOrDefault();

                if (obj == null)
                {
                    AccountEducation a = new AccountEducation();
                    a.AccountId = ent.AccountId;
                    a.SchoolId = ent.SchoolId;
                    a.Description = ent.Description;
                    a.TimespanUnit = ent.TimespanUnit;
                    a.Timespan = ent.Timespan;
                    a.YearStart = ent.YearStart;

                    _unitOfWork.AccountEducationDao.Add(a);
                }
                else
                {
                    obj.Description = ent.Description;
                    obj.TimespanUnit = ent.TimespanUnit;
                    obj.Timespan = ent.Timespan;
                    obj.YearStart = ent.YearStart;
                    _unitOfWork.AccountEducationDao.Update(obj);
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
