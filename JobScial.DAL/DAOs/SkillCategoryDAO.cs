using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class SkillCategoryDAO
    {
        private JobSocialContext _dbContext;

        public SkillCategoryDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<SkillCategory> GetSkillCategoryByName(string? name)
        {

            try
            {
                return await this._dbContext.SkillCategories
                    .SingleOrDefaultAsync(a => a.Name.Contains(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
