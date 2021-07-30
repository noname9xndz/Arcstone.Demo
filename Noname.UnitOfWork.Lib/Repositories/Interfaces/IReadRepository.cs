using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Noname.UnitOfWork.Lib.Paging;

namespace Noname.UnitOfWork.Lib.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface with READ only operations
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public interface IReadRepository<TEntity> where TEntity : class
    {
        #region Get And GetAll

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAll(bool asNoTracking = false);

        /// <summary>
        /// Get all the records from the database table
        /// </summary>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="skip">The number of records to be skipped</param>
        /// <param name="take">The number of records required</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <returns>The list of entities</returns>
        IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null,
            int? take = null, bool asNoTracking = false);

        /// <summary>
        /// Asyncronously get all the records from the database table
        /// </summary>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="skip">The number of records to be skipped</param>
        /// <param name="take">The number of records required</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The list of entities</returns>
        Task<IQueryable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null,
            int? skip = null, int? take = null, bool asNoTracking = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get all records from the database table for the given filter condition
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="skip">The number of records to be skipped</param>
        /// <param name="take">The number of records required</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <returns>The list of entities from the table</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = false);

        /// <summary>
        /// Get all records from the database table for the given filter condition (Async)
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="skip">The number of records to be skipped</param>
        /// <param name="take">The number of records required</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The list of entities from the table</returns>
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = false,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a record from the database table for the given filter condition
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <returns>The entity from the table</returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, bool asNoTracking = false);

        /// <summary>
        /// Get a record from the database table for the given filter condition (async)
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity from the table</returns>
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null,
            bool asNoTracking = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a first record from the database table for the given filter condition
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <returns>The entity from the table</returns>
        TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, bool asNoTracking = false);

        /// <summary>
        /// Get a first record from the database table for the given filter condition (async)
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity from the table</returns>
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, bool asNoTracking = false, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a record from the database table for the specific id
        /// </summary>
        /// <param name="id">The primary key or id to filter a record</param>
        /// <returns>The entity from the table</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Get a record from the database table for the specific id (async)
        /// </summary>
        /// <param name="id">The primary key or id to filter a record</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity from the table</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get And GetAll

        #region Count and check Exists

        /// <summary>
        /// Get the count of records for the specified filter condition
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <returns>Number of records</returns>
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Get the count of records for the specified filter condition (async)
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Number of records</returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Check if the record exists in database
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <returns>Returns TRUE if record exists otherwise FALSE</returns>
        bool GetExists(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Check if the record exists in database (async)
        /// </summary>
        /// <param name="filter">The filter condition</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns TRUE if record exists otherwise FALSE</returns>
        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null,string includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> selector = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IPaginate<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken));

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
        IPaginate<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true);

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
        IPaginate<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true) where TResult : class;

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
        Task<IPaginate<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false);

        #endregion Paging

        #region SQL

        //IQueryable<TEntity> Query(string sql, params object[] parameters);

        #endregion SQL
    }
}