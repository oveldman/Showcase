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
using Swashbuckle.AspNetCore.Swagger;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.Managers.Interfaces;
using Web.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            string securityKey = Configuration.GetSection("Secrets:AuthenicationKey")?.Value;
            string issuer = Configuration.GetSection("Settings:Authentication:IssuerUrl")?.Value;

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddJwtBearer(options =>  
                { 
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters  
                    {  
                        ValidateIssuer = true,  
                        ValidateAudience = true,  
                        ValidateLifetime = true,  
                        ValidateIssuerSigningKey = true,  
        
                        ValidIssuer = issuer,  
                        ValidAudience = issuer,  
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))  
                    };  
                }); 

            services.AddAuthorization(options => 
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            }); 

            services.AddScoped<IAuthenticationManager, ShowCaseAuthenticationManager>(serviceProvider => 
            {
                SignInManager<ShowCaseUser> signInManager = serviceProvider.GetService<SignInManager<ShowCaseUser>>();
                return new ShowCaseAuthenticationManager(issuer, securityKey, signInManager);
            });

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

            SetSecurity(env.IsDevelopment());
        }

        private void AddScope(IServiceCollection services) 
        {
            services.AddScoped<IResumeDB, ResumeDB>();
            services.AddScoped<IResumeManager, ResumeManager>();
        }

        private void SetSecurity(bool isDevelopment) {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
    }
}
