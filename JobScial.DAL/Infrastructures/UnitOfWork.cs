using JobScial.BAL.DTOs.Accounts;
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
        private AccountDao _accountDao;
        private LikeDAO _likeDAO;
        private PostPhotoDAO _postPhotoDAO;
        private SkillCategoryDAO _skillCategoryDAO;
        private PostSkillDao _postSkillDao;
        private PostCategoryDAO _PostCategoryDao;
        private JobTitleDAO _JobTitleDAO;




        private AccountCertificateDao _accountCertificateDao;
        private AccountSkillDao _accountSkillDao;
        private AccountEducationDao _accountEducationDao;
        private AccountExperienceDao _accountExperienceDao;
        private PostCategoryDao _postCategoryDao;
        private CompanyDao _companyDao;
        private JobTitleDao _jobTitleDao;
        private SkillDao _skillDao;
        private SchoolDao _schoolDao;
        private TimeSpanUnitDao _timeSpanUnitDao;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbContext = dbFactory.InitDbContext();
        }
        public PostCategoryDAO PostCategoryDA0
        {
            get
            {
                if (_PostCategoryDao == null)
                {
                    _PostCategoryDao = new PostCategoryDAO(_dbContext);
                }
                return _PostCategoryDao;
            }
        }

        public PostCategoryDao PostCategoryDao
        {
            get
            {
                if (_postCategoryDao == null)
                {
                    _postCategoryDao = new PostCategoryDao(_dbContext);
                }
                return _postCategoryDao;
            }
        }
        public JobTitleDAO JobTitleDao
        {
            get
            {
                if (_JobTitleDAO == null)
                {
                    _JobTitleDAO = new JobTitleDAO(_dbContext);
                }
                return _JobTitleDAO;
            }
        }

        public SkillCategoryDAO SkillCategoryDAO
        {
            get
            {
                if (_skillCategoryDAO == null)
                {
                    _skillCategoryDAO = new SkillCategoryDAO(_dbContext);
                }
                return _skillCategoryDAO;
            }
        }
        public AccountExperienceDao AccountExperienceDao
        {
            get
            {
                if (_accountExperienceDao == null)
                {
                    _accountExperienceDao = new AccountExperienceDao(_dbContext);
                }
                return _accountExperienceDao;
            }
        }
        public PostSkillDao PostSkillDao
        {
            get
            {
                if (_postSkillDao == null)
                {
                    _postSkillDao = new PostSkillDao(_dbContext);
                }
                return _postSkillDao;
            }
        }
        public AccountEducationDao AccountEducationDao
        {
            get
            {
                if (_accountEducationDao == null)
                {
                    _accountEducationDao = new AccountEducationDao(_dbContext);
                }
                return _accountEducationDao;
            }
        }
        public AccountSkillDao AccountSkillDao
        {
            get
            {
                if (_accountSkillDao == null)
                {
                    _accountSkillDao = new AccountSkillDao(_dbContext);
                }
                return _accountSkillDao;
            }
        }

        public AccountCertificateDao AccountCertificateDao
        {
            get
            {
                if (_accountCertificateDao == null)
                {
                    _accountCertificateDao = new AccountCertificateDao(_dbContext);
                }
                return _accountCertificateDao;
            }
        }

        public AccountDao AccountDao
        {
            get
            {
                if (_accountDao == null)
                {
                    _accountDao = new AccountDao(_dbContext);
                }
                return _accountDao;
            }
        }
        public PostPhotoDAO PostPhotoDAO
        {
            get
            {
                if (_postPhotoDAO == null)
                {
                    _postPhotoDAO = new PostPhotoDAO(_dbContext);
                }
                return _postPhotoDAO;
            }
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
        public LikeDAO likeDAO
        {
            get
            {
                if (_likeDAO == null)
                {
                    _likeDAO = new LikeDAO(_dbContext);
                }
                return _likeDAO;
            }
        }

        public CompanyDao CompanyDAO
        {
            get
            {
                if (_companyDao == null)
                {
                    _companyDao = new CompanyDao(_dbContext);
                }
                return _companyDao;
            }
        }

        public SchoolDao SchoolDAO
        {
            get
            {
                if (_schoolDao == null)
                {
                    _schoolDao = new SchoolDao(_dbContext);
                }
                return _schoolDao;
            }
        }

        public JobTitleDao JobTitleDAO
        {
            get
            {
                if (_jobTitleDao == null)
                {
                    _jobTitleDao = new JobTitleDao(_dbContext);
                }
                return _jobTitleDao;
            }
        }

        public SkillDao SkillDAO
        {
            get
            {
                if (_skillDao == null)
                {
                    _skillDao = new SkillDao(_dbContext);
                }
                return _skillDao;
            }
        }

        public TimeSpanUnitDao TimeSpanUnitDAO
        {
            get
            {
                if (_timeSpanUnitDao == null)
                {
                    _timeSpanUnitDao = new TimeSpanUnitDao(_dbContext);
                }
                return _timeSpanUnitDao;
            }
        }

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
