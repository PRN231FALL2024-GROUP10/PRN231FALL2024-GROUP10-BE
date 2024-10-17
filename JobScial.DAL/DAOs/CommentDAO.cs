using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class CommentDAO
    {
        private JobSocialContext _dbContext;

        public CommentDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddNewComment(Comment comment)
        {
            try
            {
                await _dbContext.AddAsync(comment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Comment>> GetComments()
        {
            try
            {

                List<Comment> comments = await _dbContext.Comments
                    .ToListAsync();
                return comments;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Comment> GetCommentById(int id)
        {
            try
            {

                Comment comments = await _dbContext.Comments
                    .SingleOrDefaultAsync(c => c.CommentId == id);
                return comments;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Update post
        public async Task UpdateComment(Comment comment)
        {
            try
            {
                _dbContext.Entry<Comment>(comment).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DeletePost
        public async Task DeleteComment(Comment comment)
        {
            try
            {
                this._dbContext.Comments.Remove(comment);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
