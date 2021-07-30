using Microsoft.EntityFrameworkCore;

namespace Arcstone.Demo.Application.AppContext.Base
{
    public abstract class AppDbContextBase : DbContext
    {
        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }
    }
}