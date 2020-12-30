using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using BusinessLayer.Manager;
using DataLayer.Process.Interface;
using DataLayer.Process;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication;

namespace Web
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });

            services.AddDbContext<ShowCaseContext>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("Web"))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine));

            services.AddDefaultIdentity<ShowCaseUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ShowCaseContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ShowCaseUser, ShowCaseContext>()
                .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            AddScope(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddScope(IServiceCollection services) 
        {
            services.AddScoped<IResumeDB, ResumeDB>();
            services.AddScoped<IResumeManager, ResumeManager>();
        }
    }
}
