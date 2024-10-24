using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class GroupDAO
    {
        private JobSocialContext _dbContext;

        public GroupDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddNewGroup(Group group)
        {
            try
            {
                await _dbContext.AddAsync(group);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
