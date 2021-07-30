using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Noname.UnitOfWork.Lib.Initializer
{
    /// <inheritdoc />
    public class DbInitializer<T> : IDbInitializer<T> where T : DbContext
    {
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopeFactory"></param>
        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        /// <inheritdoc />
        public virtual async Task Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<T>())
                {
                    context.Database.Migrate();
                }
            }

            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual async Task SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                //using (var context = serviceScope.ServiceProvider.GetService<T>())
                //{
                //    context.SaveChanges();
                //}
            }
            await Task.CompletedTask;
        }

        /// <inheritdoc />
        public IServiceScopeFactory GetServiceScopeFactory()
        {
            return _scopeFactory;
        }

    }
}
