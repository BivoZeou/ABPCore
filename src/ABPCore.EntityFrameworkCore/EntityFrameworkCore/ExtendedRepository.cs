using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;

namespace ABPCore.EntityFrameworkCore
{
    public abstract class ExtendedRepository<TEntity> : IExtendedRepository<TEntity>, IExtendedRepository, ITransientDependency where TEntity : class, IDbEntity
    {
        public abstract IQueryable<TEntity> GetAll();

        public virtual List<TEntity> GetAllList()
        {
            return this.GetAll().ToList<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult<List<TEntity>>(this.GetAllList());
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Where(predicate).ToList<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<List<TEntity>>(this.GetAllList(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(this.GetAll());
        }


        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<TEntity>(this.Single(predicate));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<TEntity>(this.FirstOrDefault(predicate));
        }

        public abstract TEntity Insert(TEntity entity);

        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.Insert(entity));
        }

        public abstract TEntity InsertAndGetSavedEntity(TEntity entity);

        public virtual Task<TEntity> InsertAndGetSavedEntityAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.InsertAndGetSavedEntity(entity));
        }

        public abstract TEntity Update(TEntity entity);

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.Update(entity));
        }

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            this.Delete(entity);
            return Task.FromResult<int>(0);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (TEntity entity in this.GetAll().Where(predicate).ToList<TEntity>())
            {
                this.Delete(entity);
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (TEntity entity in this.GetAll().Where(predicate).ToList<TEntity>())
            {
                await this.DeleteAsync(entity);
            }
        }

        public virtual int Count()
        {
            return this.GetAll().Count<TEntity>();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult<int>(this.Count());
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.GetAll().Where(predicate).Count<TEntity>();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<int>(this.Count(predicate));
        }

        public abstract List<TEntity> GetEntitiesBySqlQuery(string sql, params object[] paras);

        public abstract Task<List<TEntity>> GetEntitiesBySqlQueryAsync(string sql, params object[] paras);

    }
}
