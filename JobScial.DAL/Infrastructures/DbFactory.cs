using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Infrastructures
{
    public class DbFactory : Disposable, IDbFactory
    {
        private JobSocialContext _dbContext;

        public DbFactory()
        {

        }

        public JobSocialContext InitDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new JobSocialContext();
            }
            return _dbContext;
        }

        protected override void DisposeCore()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }
    }
}
