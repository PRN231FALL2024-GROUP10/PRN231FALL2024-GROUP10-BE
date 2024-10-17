using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class PostCategoryDAO
    {
        private JobSocialContext _dbContext;

        public PostCategoryDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task AddPostCategory(PostCategory post)
        {
            try
            {
                var maxCategoryId = await _dbContext.PostCategories
                                     .MaxAsync(c => (int?)c.PostCategoryId) ?? 0;

                post.PostCategoryId = maxCategoryId + 1;

                await _dbContext.AddAsync(post);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
