using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noname.UnitOfWork.Lib.Repositories.Implementation;
using Noname.UnitOfWork.Lib.Repositories.Interfaces;

namespace Noname.UnitOfWork.Lib
{
    /// <summary>
    /// Unit of work implementation
    /// </summary>
    /// <typeparam name="TContext">The database context</typeparam>
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        /// <summary>
        /// Holds collection of _repositories
        /// </summary>
        //private ConcurrentDictionary<string, object> _repositories;
        private Dictionary<Type, object> _repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        public UnitOfWork(TContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context), "The database context is null");
        }

        /// <summary>
        /// Gets database context object
        /// </summary>
        public TContext Context { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources database context.
        /// </summary>
        public void Dispose()
        {
            Context?.Dispose();
        }

        /// <inheritdoc />
        public int SaveChanges()
        {
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    int updateCount = this.Context.SaveChanges();
                    transaction.Commit();
                    return updateCount;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <inheritdoc />
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    int updateCount = await this.Context
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);

                    transaction.Commit();
                    return updateCount;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public dynamic CurrentContext()
        {
            return Context;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public ICrudRepository<TEntity> GetCrudRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new CrudRepository<TEntity>(Context);
                return (ICrudRepository<TEntity>)_repositories[type];
            }

            //var typeRp = typeof(IRepository<TEntity>);
            //if (!_repositories.ContainsValue(typeRp))
            //{
            //    _repositories[type] = new Repository<TEntity>(Context);
            //}

            return (ICrudRepository<TEntity>)_repositories[type];
        }
    }
}