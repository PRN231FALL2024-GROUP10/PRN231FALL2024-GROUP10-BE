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
/*        public async Task DeleteJobtile(int? id)
        {
            try
            {
                var existingjob = await _dbContext.JobTitles.FindAsync(id);

                if (existingjob != null)
                {
                    _dbContext.JobTitles.Remove(existingjob);
                    await _dbContext.SavedChangesAsync();  // Lưu thay đổi vào database
                }
                else
                {
                    throw new Exception("jobtitle not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/
        public async Task DeleteJobtile(int? id)
        {
            try
            {
                var existing = await _dbContext.JobTitles.FindAsync(id);

                if (existing != null)
                {
                    _dbContext.JobTitles.Remove(existing);
                    await _dbContext.SaveChangesAsync();  // Lưu thay đổi vào database
                }
                else
                {
                    throw new Exception("existing not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
