using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noname.UnitOfWork.Lib.Extensions.BulkExtensions;
using Noname.UnitOfWork.Lib.Repositories.Interfaces;

namespace Noname.UnitOfWork.Lib.Repositories.Implementation
{
    /// <summary>
    /// The repository class that perform CRUD operation
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public class CrudRepository<TEntity> : ReadRepository<TEntity>, ICrudRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///  Initializes a new instance of the &lt;see cref="CrudRepository"/&gt; 
        /// </summary>
        /// <param name="dbContext"></param>
        public CrudRepository(DbContext dbContext) : base(dbContext)
        {
        }

        #region Add

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.DbSet.AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);
            return await Task.FromResult(entity);
        }

        /// <inheritdoc />
        public virtual void AddMutil(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> AddMutilAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.DbSet.AddRangeAsync(entities, cancellationToken)
                .ConfigureAwait(false);
            return await Task.FromResult(entities);
        }

        #endregion Add

        #region Delete

        /// <inheritdoc />
        public virtual async Task PermanentlyDelete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                this.DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                TEntity entity = this.DbSet.Find(id);
                if (entity != null)
                {
                   await  PermanentlyDelete(entity);
                }
            }
        }

        /// <inheritdoc />
        public virtual async Task PermanentlyDeleteMutilById(IEnumerable<object> ids)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                foreach (var id in ids)
                {
                    property.SetValue(entity, id);
                    this.DbContext.Entry(entity).State = EntityState.Deleted;
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    TEntity entity = this.DbSet.Find(id);
                    if (entity != null)
                    {
                        PermanentlyDelete(entity);
                    }
                }
            }

            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual async Task PermanentlyDeleteMutil(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual async Task PermanentlyDelete(TEntity entity)
        {
            if (this.DbContext.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            this.DbSet.Remove(entity);
            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Delete(TEntity entity)
        {
            //var existing = _dbSet.Find(entity);
            //if (existing != null)
            this.DbSet.Remove(entity);
        }

        /// <inheritdoc />
        public async Task Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                this.DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = this.DbSet.Find(id);
                if (entity != null) Delete(entity);
            }

            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public void Delete(params TEntity[] entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task TemporarilyDelete(TEntity entity)
        {
            if (this.DbContext.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            this.DbSet.Remove(entity);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task TemporarilyDelete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                TrySetProperty(entity, "IsDeleted", false);
                property.SetValue(entity, id);
                this.DbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                TEntity entity = this.DbSet.Find(id);
                if (entity != null)
                {
                    await TemporarilyDelete(entity);
                }
            }

            await Task.CompletedTask;
        }

        private bool TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task TemporarilyDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        #endregion Delete

        #region Update

        /// <inheritdoc />
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            if (!this.DbContext.Entry(entity).IsKeySet)
            {
                throw new InvalidOperationException($"The primary key was not set on the entity class {entity.GetType().Name}");
            }

            this.DbSet.Update(entity);
            return await Task.FromResult(entity);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> UpdateMutil(IEnumerable<TEntity> entities)
        {
            this.DbSet.UpdateRange(entities);
            return await Task.FromResult(entities);
        }

        #endregion Update

        #region Bulk

        /// <inheritdoc />
        public virtual async Task BulkInsert(IList<TEntity> items)
        {
            await this.DbContext.BulkInsertAsync<TEntity>(items);
        }

        /// <inheritdoc />
        public virtual async Task BulkUpdate(IList<TEntity> items)
        {
            await this.DbContext.BulkUpdateAsync(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task BulkDelete(TEntity entity)
        {
            await this.DbContext.BulkDeleteAsync<TEntity>(new List<TEntity> { entity });
        }

        /// <inheritdoc />
        public virtual async Task BulkDelete(IList<TEntity> items)
        {
            if (items == null || !items.Any())
                return;
            await this.DbContext.BulkDeleteAsync(items);
        }

        /// <inheritdoc />
        public virtual async Task<int> BulkDelete(IQueryable<TEntity> items)
        {
            return await Task.FromResult(items.BatchDeleteAsync());
        }

        /// <inheritdoc />
        public virtual async Task BulkUpdate(IQueryable<TEntity> items, Expression<Func<TEntity, TEntity>> value)
        {
            await items.BatchUpdateAsync(value);
        }

        #endregion Bulk

        #region SQL

        /// <inheritdoc />
        [Obsolete]
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return this.DbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        #endregion SQL

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.DbContext?.Dispose();
        }
    }
}