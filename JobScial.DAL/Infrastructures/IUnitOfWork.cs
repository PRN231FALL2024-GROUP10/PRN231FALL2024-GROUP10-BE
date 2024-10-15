using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Infrastructures
{
    public interface IUnitOfWork
    {
        public void Commit();
        public Task CommitAsync();
    }
}
