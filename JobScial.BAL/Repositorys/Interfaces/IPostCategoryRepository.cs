using JobScial.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IPostCategoryRepository
    {
        Task<List<PostCategory>> getAll();
        Task<PostCategory> getById(int id);
        Task add(PostCategory postCategory);
        Task update(int id, PostCategory postCategory);
        Task delete(int id);
    }
}
