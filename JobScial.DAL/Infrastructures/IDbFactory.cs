using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Infrastructures
{
    public interface IDbFactory: IDisposable
    {
        public JobSocialContext InitDbContext();
    }
}
