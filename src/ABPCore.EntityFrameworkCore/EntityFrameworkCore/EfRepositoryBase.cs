using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABPCore.EntityFrameworkCore
{
    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <see cref="!:TEntity" />.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class EfRepositoryBase<TDbContext, TEntity> : ExtendedRepository<TEntity>, IExtendedRepository<TEntity>, IExtendedRepository, ITransientDependency where TDbContext : DbContext where TEntity : class, IDbEntity
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual TDbContext Context
        {
            get
            {
                return this._dbContextProvider.GetDbContext();
            }
        }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table
        {
            get
            {
                return this.Context.Set<TEntity>();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            this._dbContextProvider = dbContextProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return this.Table;
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await this.GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.GetAll().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return this.Table.Add(entity).Entity;
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult<TEntity>(this.Table.Add(entity).Entity);
        }

        public override TEntity InsertAndGetSavedEntity(TEntity entity)
        {
            entity = this.Insert(entity);

            this.Context.SaveChanges();

            return entity;
        }

        public override async Task<TEntity> InsertAndGetSavedEntityAsync(TEntity entity)
        {
            entity = await this.InsertAsync(entity);

            await this.Context.SaveChangesAsync();

            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            this.AttachIfNot(entity);
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
            return entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            this.AttachIfNot(entity);
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
            return Task.FromResult<TEntity>(entity);
        }

        public override void Delete(TEntity entity)
        {
            this.AttachIfNot(entity);
            this.Table.Remove(entity);
        }

        public override async Task<int> CountAsync()
        {
            return await this.CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await  this.GetAll().Where(predicate).CountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!this.Table.Local.Contains(entity))
            {
                this.Table.Attach(entity);
            }
        }

        public override List<TEntity> GetEntitiesBySqlQuery(string sql, params object[] paras)
        {
            return null;
        }

        public override Task<List<TEntity>> GetEntitiesBySqlQueryAsync(string sql, params object[] paras)
        {
            return null;
        }
    }
}
