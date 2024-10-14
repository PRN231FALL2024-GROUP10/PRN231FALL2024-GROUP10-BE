using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class LikeDAO 
    {
        private JobSocialContext _dbContext;

        public LikeDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddNewLike(Like like)
        {
            try
            {
                await _dbContext.AddAsync(like);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
