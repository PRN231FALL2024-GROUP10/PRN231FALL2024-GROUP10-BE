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
    }
    public interface IAccountCertificateDao : IDao<AccountCertificate>
    {
    }
    public interface IPostSkillDao : IDao<PostSkill>
    {
    }
    public interface IAccountEducationDao : IDao<AccountEducation>
    {
    }

    public interface IAccountExperienceDao : IDao<AccountExperience>
    {
    }

    public interface IAccountSkillDao : IDao<AccountSkill>
    {
        IEnumerable<AccountSkill> GetSkillsByAccountId(int accountId);
    }
    public interface IPostCategoryDao : IDao<PostCategory>
    {
    }

    public interface ICompanyDao : IDao<Company>
    {
    }

    public interface IJobTitleDao : IDao<JobTitle>
    {
    }

    public interface ISkillDao : IDao<SkillCategory>
    {
    }

    public interface ITimeSpanUnitDao : IDao<TimespanUnit>
    {
    }

    public interface ISchoolDao : IDao<School>
    {
    }
}
