using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Noname.UnitOfWork.Lib.Paging;
using Noname.UnitOfWork.Lib.Repositories.Interfaces;

namespace Noname.UnitOfWork.Lib.Repositories.Implementation
{
    /// <summary>
    /// The repository class that performs all read operations
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public class ReadRepository<TEntity> : RepositoryBase<TEntity>, IReadRepository<TEntity> where TEntity : class
    {
        #region Get And GetAll

        /// <summary>
        /// Initializes a new instance of the <see>
        ///     <cref>ReadRepository</cref>
        /// </see>
        /// class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public ReadRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = false)
        {
            return this.GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking);
        }


        /// <inheritdoc />
        public virtual async Task<IQueryable<TEntity>> GetAll(bool asNoTracking = false)
        {
            var query = this.DbSet.AsQueryable();
            if (asNoTracking)
            {
                return query.AsNoTracking<TEntity>();
            }

            return await Task.FromResult(query);
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = false)
        {
            return this.GetQueryable(null, orderBy, includeProperties, skip, take, asNoTracking);
        }

        /// <inheritdoc />
        public virtual async Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.FromResult(this.GetQueryable(null, orderBy, includeProperties, skip, take, asNoTracking));
        }

        /// <inheritdoc />
        public virtual async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null,
            int? take = null, bool asNoTracking = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.FromResult(this.GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking));
        }

        /// <inheritdoc />
        public TEntity GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.DbSet
                //.FindAsync(id, cancellationToken)
                .FindAsync(new object[] { id }, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, bool asNoTracking = false)
        {
            return this.GetQueryable(filter, orderBy, includeProperties, asNoTracking: asNoTracking)
                .FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, bool asNoTracking = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter, orderBy, includeProperties, asNoTracking: asNoTracking)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public TEntity GetOne(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null,
            bool asNoTracking = false)
        {
            return this.GetQueryable(filter, includeProperties: includeProperties, asNoTracking: asNoTracking)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null,
            bool asNoTracking = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter, includeProperties: includeProperties, asNoTracking: asNoTracking)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion Get And GetAll

        #region Count and check Exists

        /// <inheritdoc />
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter,null, includeProperties)
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.GetQueryable(filter).Any();
        }

        /// <inheritdoc />
        public async Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter, null, includeProperties)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public bool Exists(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
            {
                return this.DbSet.Any();
            }
            else
            {
                return this.DbSet.Any(selector);
            }
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null)
        {
            if (selector == null)
            {
                return await this.DbSet.AnyAsync();
            }
            else
            {
                return await this.DbSet.AnyAsync(selector);
            }
        }

        /// <inheritdoc />
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return this.DbSet.Count();
            }
            else
            {
                return this.DbSet.Count(predicate);
            }
        }

        /// <inheritdoc />
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await this.DbSet.CountAsync();
            }
            else
            {
                return await this.DbSet.CountAsync(predicate);
            }
        }

        #endregion Count and check Exists

        #region Paging

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public IPaginate<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int index = 0,
           int size = 20, bool disableTracking = true)
        {
            IQueryable<TEntity> query = this.DbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).ToPaginate(index, size) : query.ToPaginate(index, size);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="disableTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IPaginate<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = this.DbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToPaginateAsync(index, size, 0, cancellationToken);
            return await query.ToPaginateAsync(index, size, 0, cancellationToken);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public IPaginate<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true) where TResult : class
        {
            IQueryable<TEntity> query = this.DbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).ToPaginate(index, size)
                : query.Select(selector).ToPaginate(index, size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="disableTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <returns></returns>
        public async Task<IPaginate<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = this.DbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToPaginateAsync(pageIndex, pageSize, 0, cancellationToken);
            }
            else
            {
                return await query.Select(selector).ToPaginateAsync(pageIndex, pageSize, 0, cancellationToken);
            }
        }

        #endregion Paging

        #region SQL

        //public virtual IQueryable<TEntity> Query(string sql, params object[] parameters)
        //{
        //    RawSqlString query = new RawSqlString(sql);
        //    var data = DbSet.FromSql(query, parameters);
        //    return data;
        //}

        #endregion SQL
    }
}