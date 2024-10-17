using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class JobTitleDAO
    {
        private JobSocialContext _dbContext;

        public JobTitleDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddJobTitle(JobTitle job)
        {
            try
            {
                var maxJobId = await _dbContext.JobTitles
                                  .MaxAsync(j => (int?)j.JobId) ?? 0;

                job.JobId = maxJobId + 1;

                await _dbContext.AddAsync(job);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
