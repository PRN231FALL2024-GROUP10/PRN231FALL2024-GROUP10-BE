using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Implementations
{
    public class PostCategoryRepository : IPostCategoryRepository
    {
        private UnitOfWork _unitOfWork;
        public PostCategoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public async Task<List<PostCategory>> getAll()
        {
            return await _unitOfWork.PostCategoryDao.GetAll().ToListAsync();
        }

        public async Task<PostCategory> getById(int id)
        {
            return  _unitOfWork.PostCategoryDao.FindOne(p=>p.PostCategoryId == id);
        }

        public async Task add(PostCategory postCategory)
        {
            _unitOfWork.PostCategoryDao.Add(postCategory);
            _unitOfWork.Commit();
        }

        public async Task update(int id, PostCategory updatedPostCategory)
        {
            var existingPostCategory =  _unitOfWork.PostCategoryDao.FindOne(p => p.PostCategoryId == id);
            if (existingPostCategory != null)
            {
                existingPostCategory.Name = updatedPostCategory.Name;

                _unitOfWork.PostCategoryDao.Update(existingPostCategory);
                _unitOfWork.Commit();
            }
        }

        public async Task delete(int id)
        {
            var postCategory =  _unitOfWork.PostCategoryDao.FindOne(p => p.PostCategoryId == id);
            if (postCategory != null)
            {
                _unitOfWork.PostCategoryDao.Delete(postCategory);
                _unitOfWork.Commit();
            }
        }
    }
}
