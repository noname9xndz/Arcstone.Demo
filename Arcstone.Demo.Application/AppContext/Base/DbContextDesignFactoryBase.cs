using Arcstone.Demo.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Arcstone.Demo.Application.AppContext.Base
{
    public abstract class DbContextDesignFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(System.AppContext.BaseDirectory)
                ?.GetConnectionString("sqlServer");

            Console.WriteLine($"Connection String: {connString}");

            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(connString ?? throw new InvalidOperationException());

            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
        }
    }
}