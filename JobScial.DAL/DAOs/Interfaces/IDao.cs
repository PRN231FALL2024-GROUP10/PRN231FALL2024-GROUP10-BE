using JobScial.DAL.Models;
using System.Linq.Expressions;

namespace JobScial.DAL.DAOs.Interfaces
{
    public interface IDao<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(FindOptions? findOptions = null);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);
        void Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
    public interface IAccountDao : IDao<Account>
    {
        // Additional methods specific to Account can be declared here
    }
    public interface IAccountCertificateDao : IDao<AccountCertificate>
    {
        // Additional methods specific to AccountCertificate can be declared here
    }

    public interface IAccountEducationDao : IDao<AccountEducation>
    {
        // Additional methods specific to AccountEducation can be declared here
    }

    public interface IAccountExperienceDao : IDao<AccountExperience>
    {
        // Additional methods specific to AccountExperience can be declared here
    }

    public interface IAccountSkillDao : IDao<AccountSkill>
    {
        // Additional methods specific to AccountSkill can be declared here
        IEnumerable<AccountSkill> GetSkillsByAccountId(int accountId);
    }
}
