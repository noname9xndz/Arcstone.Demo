using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Noname.UnitOfWork.Lib.Initializer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbInitializer<T> where T : DbContext
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        Task Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        Task SeedData();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IServiceScopeFactory GetServiceScopeFactory();
    }
}
