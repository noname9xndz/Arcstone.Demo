using Arcstone.Demo.Application.AppContext.Base;
using Arcstone.Demo.Application.Models.Configurations;
using Arcstone.Demo.Application.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arcstone.Demo.Application.AppContext
{
    //EntityFrameworkCore\Add-Migration Init -o AppContext/Migrations
    //EntityFrameworkCore\Update-Database
    public class ArcstoneContext : AppDbContextBase
    {
        public ArcstoneContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
        }

        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

    }

    public class ArcstoneDesignFactory : DbContextDesignFactoryBase<ArcstoneContext>
    {
    }
}