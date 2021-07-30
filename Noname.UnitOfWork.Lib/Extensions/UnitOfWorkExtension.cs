using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Noname.UnitOfWork.Lib.Initializer;
using Noname.UnitOfWork.Lib.Repositories.Interfaces;

namespace Noname.UnitOfWork.Lib.Extensions
{
    /// <summary>
    /// Extension class to add unit of work middleware
    /// </summary>
    public static class UnitOfWorkExtension
    {
        /// <summary>
        /// Extension method to add unit of work to middleware
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(
            this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3, TContext4>(
            this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
            where TContext4 : DbContext
        {
            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();
            services.AddScoped<IUnitOfWork<TContext4>, UnitOfWork<TContext4>>();

            return services;
        }

        /// <summary>
        /// for using this service you can add it to your service collection
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="pattern"></param>
        public static IServiceCollection AddDbInitializer<TContext>(this IServiceCollection services, string pattern)
            where TContext : DbContext
        {

            //services.AddScoped(typeof(IDbInitializer<>));
            services.AddScoped(typeof(IDbInitializer<>), typeof(DbInitializer<>));
            // services.AddInstanceDbInitializer<TContext>(pattern);
            return services;
        }

        /// <summary>
        /// if you want to use this service every time my program starts i'm using injected service this way
        /// </summary>
        /// <param name="app"></param>
        /// <typeparam name="TContext"></typeparam>
        public static IApplicationBuilder AddDbInitializer<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer<TContext>>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }

            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="pattern"></param>
        private static void AddInstanceDbInitializer<TContext>(this IServiceCollection services, string pattern) where TContext : DbContext
        {
            var nameBase = nameof(DbInitializer<TContext>).ToLower();
            List<Assembly> allAssemblies = new List<Assembly>();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, pattern).ToList();
            referencedPaths.ForEach(path => allAssemblies.Add(Assembly.LoadFile(path)));

            var returnAssemblies = allAssemblies
                .Where(w => w.GetTypes().Any(t => t.BaseType != null && (t.IsClass
                                                                         && !t.IsAbstract
                                                                         && t.BaseType.Name.ToLower().Contains(nameBase))
                                                  ));

            foreach (var asm in returnAssemblies)
            {

                var classes = asm.GetTypes().Where(t => t.BaseType != null
                                                        && (t.IsClass && !t.IsAbstract && t.BaseType.Name.ToLower().Contains(nameBase)));
                foreach (var type in classes)
                {
                    services.AddScoped(typeof(IDbInitializer<TContext>), type);
                }
            }
        }
    }
}