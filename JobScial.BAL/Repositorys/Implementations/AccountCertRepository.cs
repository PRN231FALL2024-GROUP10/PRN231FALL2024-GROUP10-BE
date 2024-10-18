using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Implementations
{
    public class AccountCertRepository : IAccountCertRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AccountCertRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;

        }
        public async Task<bool> Delete(int AccountId, int Id)
        {
            try
            {
                var ent = _unitOfWork.AccountCertificateDao.GetAll().Where(x => x.AccountId == AccountId && x.Index == Id).FirstOrDefault();

                if (ent == null)
                {
                    return false;
                }

                _unitOfWork.AccountCertificateDao.Delete(ent);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<List<AccountCertificateDto>>  GetAll(int AccountId)
        {
            var lstAll = _unitOfWork.AccountCertificateDao.GetAll().Where(x => x.AccountId == AccountId).ToList();
            List<AccountCertificateDto> result = new List<AccountCertificateDto>();

            foreach (var ent in lstAll)
            {
                AccountCertificateDto cert = new AccountCertificateDto();
                cert.AccountId = ent.AccountId;
                cert.Index = ent.Index;
                cert.Link = ent.Link + "";
                result.Add(cert);
            }

            return result;
        }

        public async Task<AccountCertificateDto>  GetByID(int AccountId, int Id)
        {
            var ent = _unitOfWork.AccountCertificateDao.GetAll().Where(x => x.AccountId == AccountId).FirstOrDefault();
            AccountCertificateDto cert = new AccountCertificateDto();
            cert.AccountId = ent.AccountId;
            cert.Index = ent.Index;
            cert.Link = ent.Link + "";

            return cert;
        }

        public async Task<bool> Save(AccountCertificateDto ent)
        {
            try
            {
                AccountCertificate a = new AccountCertificate();
                a.AccountId = ent.AccountId;
                a.Index = _unitOfWork.AccountCertificateDao
                    .GetAll()
                    .Where(x => x.AccountId == ent.AccountId)
                    .Last().Index + 1;
                a.Link = ent.Link;

                _unitOfWork.AccountCertificateDao.Add(a);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
