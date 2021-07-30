using Arcstone.Demo.Application.AppContext;
using Arcstone.Demo.Application.Behavior;
using Arcstone.Demo.Application.Fake;
using Arcstone.Demo.Application.Models.Others;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Noname.UnitOfWork.Lib.Extensions;
using Noname.UnitOfWork.Lib.Initializer;
using System;
using System.Text;

namespace Arcstone.Demo.Api.Extensions
{
    public static class CustomServiceExtension
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string sqlServerConnectionStr = configuration.GetConnectionString("SqlServer");
            services.AddDbContextPool<ArcstoneContext>(options
                    => options.UseSqlServer(sqlServerConnectionStr))
                .AddUnitOfWork<ArcstoneContext>();
            services.AddDbInitializer<ArcstoneContext>("Arcstone.Demo.*.dll");
            services.AddScoped(typeof(IDbInitializer<ArcstoneContext>), typeof(ArcstoneInitializer));

            return services;
        }

        public static IServiceCollection AddAuthJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var audience = new ConfigAudience();
            configuration.GetSection("Audience").Bind(audience);
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audience.Secret)),
                ValidateIssuer = true,
                ValidIssuer = audience.Iss,
                ValidateAudience = true,
                ValidAudience = audience.Aud,
                ValidateLifetime = true
            };

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                });
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddCustomAddMediatR(this IServiceCollection services)
        {
            var assemblyApplication = AppDomain.CurrentDomain.Load("Arcstone.Demo.Application");
            services.AddMediatR(assemblyApplication)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            ;
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Arcstone.Demo.Api",
                    Description = "Arcstone.Demo.Api Swagger",
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}