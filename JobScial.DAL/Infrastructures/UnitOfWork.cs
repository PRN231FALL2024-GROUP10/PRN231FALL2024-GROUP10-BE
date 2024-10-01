using JobScial.DAL.DAOs;
using JobScial.DAL.Models;
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
        
        public UnitOfWork(IDbFactory dbFactory)
        {
            if (this._dbContext == null)
            {
                this._dbContext = dbFactory.InitDbContext();
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
