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

        public async Task<SkillCategory> GetById(int? id)
        {

            try
            {
                return await this._dbContext.SkillCategories
                    .SingleOrDefaultAsync(a => a.SkillCategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PostSkill>> GetAllById(int id)
        {
            try
            {
                return await _dbContext.PostSkills
                    .Where(x => x.PostId == id)
                    .Select(x => new
                    {
                        x.PostId,
                        x.SkillCategoryId,

                    })
                    .Distinct() // Loại bỏ các bản ghi trùng lặp
                    .Select(x => new PostSkill // Tạo lại đối tượng NewsTagg mới từ các thuộc tính
                    {
                        PostId = x.PostId,
                        SkillCategoryId = x.SkillCategoryId,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
