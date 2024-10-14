using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class PostPhotoDAO
    {
        private JobSocialContext _dbContext;

        public PostPhotoDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddNewPostPhoto(PostPhotos postPhotos)
        {
            try
            {
                await _dbContext.AddAsync(postPhotos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
