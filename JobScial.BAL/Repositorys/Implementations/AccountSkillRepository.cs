using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Implementations
{
    public class AccountSkillRepository : IAccountSkillRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AccountSkillRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;

        }
        public async Task<bool> Delete(int AccountId, int Id)
        {
            try
            {
                var ent = _unitOfWork.AccountSkillDao.GetAll().Where(x => x.AccountId == AccountId && x.SkillCategoryId == Id).FirstOrDefault();

                if (ent == null)
                {
                    return false;
                }

                _unitOfWork.AccountSkillDao.Delete(ent);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<List<AccountSkillDto>>  GetAll(int AccountId)
        {
            var lstAll = _unitOfWork.AccountSkillDao.GetAll().Where(x => x.AccountId == AccountId).ToList();
            List<AccountSkillDto> result = new List<AccountSkillDto>();

            foreach (var ent in lstAll)
            {
                AccountSkillDto cert = new AccountSkillDto();
                cert.AccountId = ent.AccountId;
                cert.SkillCategoryId = ent.SkillCategoryId;
                cert.Description = ent.Description;
                cert.TimespanUnit = ent.TimespanUnit;
                cert.SkillLevel = ent.SkillLevel;
                cert.Timespan = ent.Timespan;

                result.Add(cert);
            }

            return result;
        }

        public async Task<AccountSkillDto>  GetByID(int AccountId, int Id)
        {
            var ent = _unitOfWork.AccountSkillDao.GetAll().Where(x => x.AccountId == AccountId && x.SkillCategoryId == Id).FirstOrDefault();
            AccountSkillDto cert = new AccountSkillDto();
            if (ent == null)
            {
                return cert;
            }

            //var timespanunit = _unitOfWork.TimeSpanUnitDAO.GetAll().Where(x => x.TimespanUnitId == ent.TimespanUnit).FirstOrDefault();


            cert.AccountId = ent.AccountId;
            cert.SkillCategoryId = ent.SkillCategoryId;
            cert.Description = ent.Description;
            cert.TimespanUnit = ent.TimespanUnit;
            cert.SkillLevel = ent.SkillLevel;
            cert.Timespan = ent.Timespan;
            return cert;
        }

        public async Task<bool> Save(AccountSkillDto ent)
        {
            try
            {
                var obj = _unitOfWork.AccountSkillDao
                    .GetAll()
                    .Where(x => x.AccountId == ent.AccountId && x.SkillCategoryId == ent.SkillCategoryId).FirstOrDefault();

                if (obj == null)
                {
                    AccountSkill a = new AccountSkill();
                    a.AccountId = ent.AccountId;
                    a.SkillCategoryId = ent.SkillCategoryId;
                    a.SkillLevel = ent.SkillLevel;
                    a.Description = ent.Description;
                    a.TimespanUnit = ent.TimespanUnit;
                    a.Timespan = ent.Timespan;

                    _unitOfWork.AccountSkillDao.Add(a);
                }
                else
                {
                    obj.Description = ent.Description;
                    obj.TimespanUnit = ent.TimespanUnit;
                    obj.Timespan = ent.Timespan;
                    _unitOfWork.AccountSkillDao.Update(obj);
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
