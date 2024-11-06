using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddNewPostPhoto(PostPhoto postPhotos)
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
        public void UpdatePostPhoto(PostPhoto postPhoto)
        {
            try
            {
                this._dbContext.Entry<PostPhoto>(postPhoto).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteFirstPostPhotoByPostIdAsync(int postId)
        {
            try
            {
                // Tìm PostPhoto đầu tiên dựa trên PostId
                var postPhoto = await _dbContext.PostPhotos
                                                .FirstOrDefaultAsync(photo => photo.PostId == postId);

                if (postPhoto != null)
                {
                    // Xóa ảnh khỏi database
                    _dbContext.PostPhotos.Remove(postPhoto);
                }
                else
                {
                    throw new Exception("No photo found for this post.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<PostPhoto>> GetAllById(int id)
        {
            try
            {
                return await _dbContext.PostPhotos
                    .Where(x => x.PostId == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
