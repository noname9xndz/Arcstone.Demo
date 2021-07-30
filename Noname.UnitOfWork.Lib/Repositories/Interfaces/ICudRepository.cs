using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Noname.UnitOfWork.Lib.Repositories.Interfaces
{
    /// <summary>
    /// Interface for repository doing create, update and delete operations
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public interface ICudRepository<TEntity> where TEntity : class
    {
        #region Add

        /// <summary>
        /// Add or create new entity to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entity">New entity</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds set of new entities to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entities">List of new entity</param>
        void AddMutil(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add or create new entity to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task that is awaitable</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds set of new entities to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entities">List of new entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task that is awaitable</returns>
        Task<IEnumerable<TEntity>> AddMutilAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Add

        #region Update

        /// <summary>
        /// Update modified record into database table
        /// </summary>
        /// <param name="entity">The modified entity</param>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Update list of modified record into database table
        /// </summary>
        /// <param name="entities">The modified entity list</param>
        Task<IEnumerable<TEntity>> UpdateMutil(IEnumerable<TEntity> entities);

        #endregion Update

        #region Delete

        /// <summary>
        /// Delete list of entities from database table
        /// </summary>
        /// <param name="entities">Entities that are going to be deleted</param>
        Task PermanentlyDeleteMutil(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity from database table
        /// </summary>
        /// <param name="id">Entity id that is going to be deleted</param>
        Task PermanentlyDelete(object id);

        /// <summary>
        /// Delete entity from database table
        /// </summary>
        /// <param name="entity">Entity that is going to be deleted</param>
        Task PermanentlyDelete(TEntity entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task PermanentlyDeleteMutilById(IEnumerable<object> ids);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(object id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        void Delete(params TEntity[] entities);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<TEntity> entities);

        ///// <summary>
        ///// changed status : inactive entity from database table
        ///// </summary>
        ///// <param name="entity"></param>
        //Task TemporarilyDelete(TEntity entity);

        ///// <summary>
        ///// changed status : inactive entity from database table
        ///// </summary>
        ///// <param name="entity"></param>
        //Task TemporarilyDelete(object id);

        ///// <summary>
        ///// changed status : inactive entity from database table
        ///// </summary>
        ///// <param name="entity"></param>
        //Task TemporarilyDelete(IEnumerable<TEntity> entities);

        #endregion Delete

        #region Bulk

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task BulkInsert(IList<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task BulkDelete(IList<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<int> BulkDelete(IQueryable<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task BulkUpdate(IList<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task BulkUpdate(IQueryable<TEntity> items, Expression<Func<TEntity, TEntity>> value);

        #endregion Bulk

        #region SQL

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        #endregion SQL
    }
}