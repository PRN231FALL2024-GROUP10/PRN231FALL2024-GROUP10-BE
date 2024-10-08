using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs.Implements
{
    public class AccountDao : Dao<Account>, IAccountDao
    {
        public AccountDao(JobSocialContext context) : base(context) { }
        // Additional methods can be implemented here if needed
    }

    public class AccountCertificateDao : Dao<AccountCertificate>, IAccountCertificateDao
    {
        public AccountCertificateDao(JobSocialContext context) : base(context) { }
        // Additional methods can be implemented here if needed
    }

    public class AccountEducationDao : Dao<AccountEducation>, IAccountEducationDao
    {
        public AccountEducationDao(JobSocialContext context) : base(context) { }
        // Additional methods can be implemented here if needed
    }

    public class AccountExperienceDao : Dao<AccountExperience>, IAccountExperienceDao
    {
        public AccountExperienceDao(JobSocialContext context) : base(context) { }
        // Additional methods can be implemented here if needed
    }

    public class AccountSkillDao : Dao<AccountSkill>, IAccountSkillDao
    {
        public AccountSkillDao(JobSocialContext context) : base(context) { }
        // Additional methods can be implemented here if needed
    }

    public class Dao<TEntity> : IDao<TEntity> where TEntity : class
    {
        private readonly JobSocialContext _context;
        public Dao(JobSocialContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void AddMany(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Find(predicate);
            _context.Set<TEntity>().RemoveRange(entities);
        }
        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).FirstOrDefault(predicate)!;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
        {
            return Get(findOptions).Where(predicate);
        }
        public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
        {
            return Get(findOptions);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }
        private DbSet<TEntity> Get(FindOptions? findOptions = null)
        {
            findOptions ??= new FindOptions();
            var entity = _context.Set<TEntity>();
            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes().AsNoTracking();
            }
            else if (findOptions.IsIgnoreAutoIncludes)
            {
                entity.IgnoreAutoIncludes();
            }
            else if (findOptions.IsAsNoTracking)
            {
                entity.AsNoTracking();
            }
            return entity;
        }
    }
}
