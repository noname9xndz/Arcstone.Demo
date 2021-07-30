using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noname.UnitOfWork.Lib.Repositories.Interfaces;

namespace Noname.UnitOfWork.Lib
{
    /// <summary>
    /// Contract that defines set of methods to create repositories and atomic operations
    /// </summary>
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        /// <summary>
        /// Save database context changes into database
        /// </summary>
        /// <returns>Affected records count</returns>
        int SaveChanges();

        /// <summary>
        /// Save database context changes into database
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Affected records count</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        dynamic CurrentContext();
    }

    /// <summary>
    /// Contract that defines set of methods to create repositories and atomic operations
    /// </summary>
    /// <typeparam name="TContext">Database context type</typeparam>
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        /// <summary>
        /// Gets database context object
        /// </summary>
        TContext Context { get; }
    }
}