using JobScial.DAL.DAOs;
using JobScial.DAL.DAOs.Implements;
using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private JobSocialContext _dbContext;
        private AccountDAO _accountDAO;
        private PostDAO _postDAO;
        private CommentDAO _commentDAO;
        private readonly IAccountDao _accountDao;
        private readonly IAccountCertificateDao _accountCertificateDao;
        private readonly IAccountEducationDao _accountEducationDao;
        private readonly IAccountExperienceDao _accountExperienceDao;
        private readonly IAccountSkillDao _accountSkillDao;

        public UnitOfWork(IDbFactory dbFactory,
                          IAccountDao accountDao,
                          IAccountCertificateDao accountCertificateDao,
                          IAccountEducationDao accountEducationDao,
                          IAccountExperienceDao accountExperienceDao,
                          IAccountSkillDao accountSkillDao)
        {
            _dbContext = dbFactory.InitDbContext();
            _accountDao = accountDao;
            _accountCertificateDao = accountCertificateDao;
            _accountEducationDao = accountEducationDao;
            _accountExperienceDao = accountExperienceDao;
            _accountSkillDao = accountSkillDao;
        }

        public AccountDAO AccountDAO
        {
            get
            {
                if (_accountDAO == null)
                {
                    _accountDAO = new AccountDAO(_dbContext);
                }
                return _accountDAO;
            }
        }
        public CommentDAO CommentDAO
        {
            get
            {
                if (_commentDAO == null)
                {
                    _commentDAO = new CommentDAO(_dbContext);
                }
                return _commentDAO;
            }
        }
        public PostDAO PostDAO
        {
            get
            {
                if (_postDAO == null)
                {
                    _postDAO = new PostDAO(_dbContext);
                }
                return _postDAO;
            }
        }
        public IAccountDao AccountDao => _accountDao;
        public IAccountCertificateDao AccountCertificateDao => _accountCertificateDao;
        public IAccountEducationDao AccountEducationDao => _accountEducationDao;
        public IAccountExperienceDao AccountExperienceDao => _accountExperienceDao;
        public IAccountSkillDao AccountSkillDao => _accountSkillDao;

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
