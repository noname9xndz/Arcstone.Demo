using Arcstone.Demo.Api.Extensions;
using Arcstone.Demo.Api.Middlewares;
using Arcstone.Demo.Application.AppContext;
using Arcstone.Demo.Application.Models.Others;
using HotelManagementSystem.Models.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Noname.UnitOfWork.Lib.Extensions;

namespace Arcstone.Demo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.Configure<ConfigAudience>(options => Configuration.GetSection("Audience").Bind(options));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddCustomAddMediatR();
            services.AddCustomDbContext(Configuration);
            services.AddCustomServices();
            services.AddCustomSwagger();
            services.AddAuthJwt(Configuration);
            services.AddControllers().AddNewtonsoftJson();
            services.AddCustomAddMediatR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder => corsBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.AddDbInitializer<ArcstoneContext>();
            app.AddSwagger();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<ErrorMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}