using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class PostDAO
    {
        private JobSocialContext _dbContext;

        public PostDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task AddNewPost(Post post)
        {
            try
            {
                await _dbContext.AddAsync(post);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Post> GetPostById(int? postId)
        {

            try
            {
                return await this._dbContext.Posts
                    .SingleOrDefaultAsync(a => a.PostID.Equals(postId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Post>> GetPosts()
        {
            try
            {

                List<Post> posts = await _dbContext.Posts
                    .AsNoTracking()
                    .Include(m => m.Likes)
                    .Include(m => m.Comments)
                    .Include(m => m.Job)
                    .Include(m => m.PostPhotos)
                    .Include(m => m.PostCategory)
                    .Include(m => m.PostSkill)
                        .ThenInclude(p => p.SkillCategory)
                    .ToListAsync();
                return posts;

            }
            catch (Exception ex)   
            {
                throw new Exception(ex.Message);
            }
        }
        #region Update post
        public async Task UpdatePost(Post post)
        {
            try
            {
                _dbContext.Entry<Post>(post).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        public async Task<List<Post>> GetPostsByUserid(int userId)
        {
            try
            {
                List<Post> posts = await _dbContext.Posts
                    .AsNoTracking()
                    .Include(m => m.Likes)
                    .Include(m => m.Comments)
                    .Include(m => m.Job)
                    .Include(m => m.PostPhotos)
                    .Include(m => m.PostCategory)
                    .Include(m => m.PostSkill)
                        .ThenInclude(p => p.SkillCategory)
                    .Where(p => p.CreatedBy == userId) // Lọc theo CreatedBy
                    .ToListAsync();

                return posts;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
